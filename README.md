# varManager
varManager
var Manager for virt-a-mate
This tool is used to manage var files.
The main method is to place all var files to the repository directory.Create a symlink link to the var file in the AddonPackages directory as needed.

---

## .NET 8 + SQLite 现代化版本

为解决在 Windows 11 上 Microsoft Access(ACE/Jet OLEDB)驱动导致的频繁报错,本分支已将主程序迁移到 **.NET 8 + 内嵌 SQLite**(免安装数据库驱动、无位数冲突),功能保持不变。

构建:`dotnet build varManager.sln -c Release`(需 .NET 8 SDK,无需 Visual Studio)。

详见 [MODERNIZATION.md](MODERNIZATION.md)。
