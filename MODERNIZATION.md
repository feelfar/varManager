# varManager 现代化说明(.NET 8 + SQLite)

本次改造的目标:**解决在 Windows 11 上因 Microsoft Access(ACE/Jet OLEDB)驱动导致的频繁报错,并把工程现代化到 .NET 8;不丢失任何现有功能,仅优化兼容性与编译方式。**

## 为什么会在 Win11 报错(根因)

旧版主程序用强类型 DataSet,通过 **`Microsoft.ACE.OLEDB.12.0`** 驱动读写 Access 的 `varManager.mdb`。
该驱动需要单独安装“Access Database Engine 可再发行组件”,且**位数必须与程序一致**(x64 程序需 x64 ACE,常与系统里 32 位 Office 冲突),于是出现
“provider not registered / 频繁报错”。`.NET Framework 4.8` 本身在 Win11 上没问题——**问题在数据库驱动**。

## 主要变更

| 项 | 旧 | 新 |
|---|---|---|
| 数据库 | Access `varManager.mdb`(ACE OLEDB,需装驱动) | **内嵌 SQLite**(`varManager.sqlite`,免安装、无位数问题) |
| 数据访问 | VS 生成的 OleDb TableAdapter | 同名 TableAdapter,改用 `System.Data.SQLite`(`SQLiteDataAdapter`+`SQLiteCommandBuilder`) |
| 主程序框架 | .NET Framework 4.8(旧式 csproj) | **.NET 8**（`net8.0-windows`，SDK 式工程，WinForms） |
| DragNDrop 控件库 | .NET Framework 4.8 | **.NET 8** |
| DgvFilterPopup 依赖 | 引用仓库外缺失项目(无法编译) | **vendored 进仓库**(CPOL 源码,重编为 .NET 8) |
| MMDLoader | .NET 6 | **.NET 8** |
| LoadScene | .NET 3.5(VaM/Unity 插件) | **保持 .NET 3.5 不变**(见下方注意事项) |

> 上层所有窗体代码**未改动**:仍调用一模一样的 `Fill / GetData / Update / FillByDepens / FillBy / FillByLoadable / DeleteAll` 等方法,只是底层从 Access 换成了 SQLite。

## 数据库与数据迁移

- 首次运行时,`Db.Initialize()` 会创建 `varManager.sqlite` 并建好全部表/索引/视图
  (schema 见 [`migration/schema.sql`](migration/schema.sql),作为嵌入资源打包)。
- **数据库位置(重要)**:程序自动选择一个可写目录——
  - exe 所在目录若是**本地可写盘**,就放在 exe 旁(便携);
  - 否则(只读目录、或 exe 在**网络盘/映射盘**上)放到 **`%LOCALAPPDATA%\varManager\`**(即 `C:\Users\<你>\AppData\Local\varManager`)。
  - 之所以这样:本程序清单要求**管理员权限**,而 Windows 提权会话**看不到普通会话里映射的网络盘**;同时 SQLite 在 SMB 网络盘上锁机制不可靠。把库放本地磁盘可彻底规避“unable to open database file”。
  - 启动报错对话框会显示实际使用的“数据目录”,便于定位。
- 两个 Access 存储查询 `installedScenes`、`varsView` 已原样复刻为 SQLite **视图**;
  `scenesView` / `HideFav` 本就是程序内存中的 DataTable,与数据库无关。
- **保留旧数据(可选)**:把旧的 `varManager.mdb` 放到上面的**数据目录**(本地安装=exe 旁;网络盘/提权场景=`%LOCALAPPDATA%\varManager\`),首次运行会自动把
  `vars / installStatus / scenes / dependencies / savedepens` 五张表导入 SQLite(`MdbToSqliteMigrator`)。
  - 读取旧 `.mdb` 仍需本机有 ACE 驱动;若没有/读取失败,**不会崩溃**:保留空库,程序通过重新扫描 var 仓库自动重建索引。
  - **隐藏/收藏(hide/fav)状态不在数据库里**——它存为 VaM `AddonPackagesFilePrefs\...\*.hide / *.fav` 标记文件,任何数据库迁移都不影响,天然保留。

## 旧数据迁移工具(界面内 + 命令行)

把旧版 Access(`varManager.mdb`)里的全部数据一次性导入新 SQLite,**可分别选择源/目标路径**,适配“旧程序在 D 盘、新程序在别处”的情况:

- **界面内**:启动 varManager → 打开 **Settings** → 左下角 **「从旧版(Access)迁移数据…」** → 对话框中:
  - 源:选旧程序的 `varManager.mdb`(或其所在文件夹);
  - 目标:默认就是当前程序使用的 SQLite 库(也可另选);
  - 勾选「迁移前清空目标表(全量覆盖)」做全量迁移 → 开始。带实时进度与逐表结果。
- **命令行**:`varManager.exe --migrate` 直接打开迁移对话框。

读取 `.mdb` 需本机有 ACE OLEDB 驱动(失败会有明确提示)。迁移**保留原始 ID**;隐藏/收藏标记本就是文件、不受影响。迁完若目标=当前库,需重启 varManager 加载新数据。

> 已用真实数据验证:导入 **357,949 行**(vars 13442 / installStatus 13430 / scenes 193876 / dependencies 129823 / savedepens 7378)**仅 2.6 秒**,目标库行数与视图(varsView/installedScenes)校验全部一致。

## 性能优化(SQLite 写路径 + 启动)

切到 SQLite 后第一版出现“启动慢、扫描时 C 盘狂读写”,根因与修复:

| 问题 | 根因 | 修复 |
|---|---|---|
| 扫描/批量编辑时系统盘 IO 极高 | 每个 `DataAdapter.Update` 的每一行都是独立 autocommit 事务 → 逐行 journal + fsync | `UpdateCore` 把整批 `Update` 包进**单个事务**;连接串加 `Journal Mode=WAL;Synchronous=Normal`(可重建的索引库,安全) |
| 每次保存都很慢 | `SQLiteCommandBuilder` 每次 Update 都重新查一遍表结构生成 CRUD | 生成的 Insert/Update/Delete 命令**缓存复用**(连接变更时失效重建) |
| Hide\|Fav(场景管理)开关后内存暴涨、卡顿 | 每个窗体一套适配器、每适配器一条连接;`Cache Size=16000` 页(≈64MB/连接)叠加连接池让关闭后的连接带着页缓存常驻进程 | **不用连接池**,`Cache Size=-8192`(8MB 上限,连接关闭即释放);适配器 `Dispose` 时确定性释放连接与缓存命令 |
| 浏览预览图后内存可达 10GB+、退出卡死 | **原版遗留缺陷,被 .NET 8 GC 行为放大**:`Image.FromFile` 的源位图(原生内存,GC 几乎感知不到)加入 ImageList 后从不 Dispose;且滚动缓存上限(40/20 张)小于一屏可见项数 → 可见项互相踢出缓存,**每次重绘都重载并泄漏整屏位图**;大预览图 `PictureBox.Image` 换图也不释放旧图 | `Comm.AddImageToList`(ImageList 内部拷贝后**立即 Dispose 源图**)、`TrimImageList`(上限 256/滞回 192,大于可见项数,杜绝互踢)、`SwapPictureBoxImage`(换图即释放旧图),覆盖 Form1 / FormScenes / FormUninstallVars 全部 8 处;FormScenes 关闭时释放预览图 |
| **关闭场景管理/退出程序时卡死数十秒**(.NET 8 独有) | .NET 8 WinForms 在**窗口销毁时**(`WmDestroy → ReleaseUiaProvider`)会把虚拟 ListView 的条目**逐项枚举一遍**——几万项 × 每项触发一次 `RetrieveVirtualItem`(读图/建项),.NET Framework 无此行为;且 `components.Dispose()` 先于子窗口销毁,销毁期重入会撞上已释放的 ImageList → `Parameter is not valid` 崩溃 | `FormClosing` 时置 `closingDown` 标志并把所有虚拟列表 `VirtualListSize = 0`(销毁期枚举变为 0 项),`RetrieveVirtualItem` 入口加关闭防卫返回空项;`AddImageToList` 先强制创建 ImageList 句柄再添加(句柄存在时 Add 立即拷贝,源图可安全释放)。实测关闭从卡死降至 **~100ms** |
| 启动卡界面 | 启动期 DB 加载 + 安装状态核对都在 UI 线程 | 全部移到后台 `BackgroundWorker`,完成后再渲染表格(`installStatus` 用单事务 `BulkReplace`,避免“全部显示未安装”的闪烁) |

**真实规模基准**(用户真实库:13.4k vars / 194k scenes / 130k deps,在 NVMe 上):

```
启动加载: vars.Fill 66ms · scenes.Fill 651ms · deps.Fill 336ms · varsView 98ms   (合计 ~1.3s)
批量写  : 改写全部 vars 232ms · 新增 1 万 scenes 176ms · 删除 1 万 171ms
          installStatus 全量 BulkReplace 1.3 万行 28ms · 500 次单行 Update 48ms
```

## UI 一致性(与原版逐像素对齐)

各窗体原本用 `AutoScaleMode.Font` + `AutoScaleDimensions=(8,17)`(按 .NET Framework 对 Cambria 9pt 的度量基准设计)。.NET 8 对同一字体的度量不同(≈7×14),Font 模式据此把**所有控件按 ~0.85 缩小**、但字号不变 → 固定尺寸按钮(如底部 70×42 双行按钮)塞不下文字而被裁切。

修复:全部窗体改 **`AutoScaleMode.None`**(原版运行期缩放因子恒为 1,None 即等价于“不缩放”),布局回到设计稿、与原版逐像素一致。已用 UI Automation 实测按钮尺寸 + 截图比对验证。

> 同时 `Program.Main` 用 `ApplicationConfiguration.Initialize()` 并在 csproj 声明 `ApplicationHighDpiMode=DpiUnaware`,与原版无 DPI 感知的行为对齐(双保险)。

## 如何构建

需要 .NET 8 SDK(无需 Visual Studio):

```powershell
# 编译整个解决方案(varManager + DragNDrop + DgvFilterPopup + MMDLoader)
dotnet build varManager.sln -c Release
```

发布可分发版本:

```powershell
# 方式 A：框架依赖(体积小,目标机需装 “.NET 8 Desktop Runtime”)
dotnet publish varManager\varManager.csproj -c Release -r win-x64 --no-self-contained -o dist\varManager-win-x64

# 方式 B：自包含(免装运行时,体积约 ~150MB,拷贝即用)
dotnet publish varManager\varManager.csproj -c Release -r win-x64 --self-contained -o dist\varManager-selfcontained
# 如需单文件，追加： -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

## 注意事项

- **LoadScene 必须保持 .NET 3.5**:它是注入 Virt-a-Mate(老版 Unity/Mono)内部运行的插件,
  引用 VaM 的 `Assembly-CSharp.dll`/`UnityEngine*.dll`。升级框架会使其无法在 VaM 内加载,
  因此本次**不改动**,也未纳入 `varManager.sln`;它与 Win11 兼容性问题无关,按原工具链单独构建即可。
- 程序清单要求**管理员权限**(创建符号链接所需),首次启动会有 UAC 提示——这是原有行为。
- 旧 `varManager.mdb` / 仓库根的空 `varManager.db` 已不再参与编译或打包,仅保留作参考。
- 数千条 `CA1416` 编译警告是“WinForms API 仅 Windows 可用”的提示,对 `net8.0-windows` 无害。

## .NET 8 行为差异:全项目系统审计(2026-06)

把所有"在 .NET Framework 能用、到 .NET 8 默认行为变了"的高发模式过了一遍。**已修复**的:

| 模式 | .NET 8 的变化 | 处理 |
|---|---|---|
| `Process.Start("https://…" / 文件夹)` | 默认 `UseShellExecute` 由 true→false,把 URL 当可执行路径 → "系统找不到指定的文件" | 统一 `Comm.OpenShell()`(显式 `UseShellExecute=true`+try/catch),替换 8 处:Hub 作品页/下载、Google 搜索缺失 var、打开 Stale/旧版本文件夹、日志 |
| `Directory.GetCurrentDirectory()` 定位 `Cache\` | 返回的是进程**工作目录**(文件对话框/提权会改它),非 exe 目录 → 缓存写一处、读/清另一处,缓存形同失效 | 统一 `Comm.CacheDir`(基于稳定的 `AppContext.BaseDirectory`),替换 5 处 |
| 默认 DPI 感知 / 字体度量 / `AutoScaleMode.Font` | 见上文 UI 章节 | 全窗体 `AutoScaleMode.None` + DpiUnaware |
| 窗体销毁期 UIA 枚举虚拟列表 / GDI 位图不释放 | 见上文内存章节 | 关闭时 `VirtualListSize=0`、`Comm` 图片助手确定性释放 |

**已审计、确认安全(无需改)**:`Clipboard.SetText`、`Settings.Default.Save()`、命名 `Mutex`/`WaitOne`/`ReleaseMutex`、`BackgroundWorker`+`e.Error`、SharpZipLib `ZipFile`、现代 `HttpClient`、P/Invoke `CreateSymbolicLink`、SimpleJSON 自有 Serialize、ISO 格式 `DateTime.Parse`、数字/日期 `ToString("…")`。
**无任何**危险 API:没有 `BinaryFormatter`/`SoapFormatter`、`WebClient`、`Registry`、`SmtpClient`、`Thread.Abort`、`AppDomain` 卸载。
**有意保留 .NET 8 行为**:读 var 包内 `meta.json` 用 `StreamReader` 默认 UTF-8——VAM 规范就是 UTF-8,这比旧框架按系统 ANSI(GBK)读更正确,不回改。

## 使用体验优化(2026-06)

针对实际使用中的三处不顺手:

| 体验问题 | 根因 | 优化 |
|---|---|---|
| 主表滚动一卡一卡 | `DataGridView` 默认不双缓冲,.NET 8 重绘下卡顿更明显 | 反射开启 `DoubleBuffered`(`EnableDoubleBuffer`),滚动平滑 |
| 一次只能 install/uninstall **500** 条、delete **50** 条 | Access 时代怕单次写太多的保护 | install/uninstall **取消硬上限**(SQLite 事务化后批量持久化很快)+ 操作时等待光标;delete 真删文件,放宽到 **5000**(防"全选误删"的保险) |
| 重建符号链接 / 修复预览图等点完不知跑没跑完,期间点别的就崩(`This BackgroundWorker is currently busy`) | ~11 个功能共用同一个 `backgroundWorkerInstall`,各按钮直接 `RunWorkerAsync` 不查 `IsBusy` | 统一入口 `RunBackgroundTask`:忙时**友好提示不崩**;运行中**标题栏显示"正在执行: xxx…"**+ 等待光标;完成自动恢复 |

## 本次改造已验证

- ✅ 解决方案 Release 全量编译:**0 错误**。
- ✅ 数据层冒烟测试([`tests/DbSmokeTest`](tests/DbSmokeTest)):用真实 `System.Data.SQLite`
  跑通建库、增删改查、两个视图、位置参数自定义查询、自增 ID、日期/布尔往返——全部通过。
- ✅ `.mdb→SQLite` 迁移测试:从 Access 读出并正确导入 SQLite,字段值与 ID 保留。
- ⚠️ 完整 GUI 行为需你在自己的 VaM 环境中实测(涉及真实 var 目录、符号链接、管理员权限)。

## 关键新增/改动文件

- `varManager/varManagerDataSet.SqliteAdapters.cs` —— 新的 SQLite TableAdapter(替换原 OleDb 适配器)
- `varManager/Db.cs` —— SQLite 引导(建库 + 触发迁移)
- `varManager/MdbToSqliteMigrator.cs` —— 旧 .mdb → SQLite 一次性导入
- `varManager/varManagerDataSet.Designer.cs` —— 截断保留 DataSet/DataTable/DataRow 类型(移除 OleDb 适配器段)
- `migration/schema.sql` —— SQLite 库结构(单一事实来源,嵌入主程序)
- `DgvFilterPopup/` —— vendored 的列筛选控件(CPOL),重编为 net8
- `migration/legacy/` —— 原始 `varManagerDataSet.Designer.cs` 与旧 `.sln` 的备份
