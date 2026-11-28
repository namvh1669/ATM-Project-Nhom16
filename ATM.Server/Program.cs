using System;
using System.Windows.Forms;

namespace ATM.Server
{
    internal static class Program
    {
        /// <summary>
        /// Tham số dòng lệnh (tùy chọn):  --port 9101  --name Server-1  --db "C:\path\atm.db"  --autostart
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
#if NETCOREAPP
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerForm(args));
        }
    }
}
