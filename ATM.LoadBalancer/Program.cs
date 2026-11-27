using System;
using System.Windows.Forms;

namespace ATM.LoadBalancer
{
    internal static class Program
    {
        /// <summary>Tham số tùy chọn: --port 9000  --autostart</summary>
        [STAThread]
        private static void Main(string[] args)
        {
#if NETCOREAPP
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LbForm(args));
        }
    }
}
