using System;

namespace mmd2timeline
{
    // Token: 0x0200002A RID: 42
    internal class LogUtil
    {
        // Token: 0x060001FB RID: 507 RVA: 0x0000ADB4 File Offset: 0x00008FB4
        public static void Log(string log)
        {
            SuperController.LogMessage(DateTime.Now.ToString("HH:mm:ss") + "【mmd2timeline】" + log);
        }

        // Token: 0x060001FC RID: 508 RVA: 0x0000ADE4 File Offset: 0x00008FE4
        public static void LogError(string log)
        {
            SuperController.LogError(DateTime.Now.ToString("HH:mm:ss") + "【mmd2timeline】" + log);
        }

        // Token: 0x060001FD RID: 509 RVA: 0x0000AE14 File Offset: 0x00009014
        public static void LogWarning(string log)
        {
            SuperController.LogMessage(DateTime.Now.ToString("HH:mm:ss") + "【mmd2timeline】 Warning:" + log);
        }
    }
}
