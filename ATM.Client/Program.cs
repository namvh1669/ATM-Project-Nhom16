using System;
using System.Windows.Forms;
using ATM.Client.Forms;
using ATM.Client.Net;

namespace ATM.Client
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
#if NETCOREAPP
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ClientConfig config = ClientConfig.Load();
            var client = new AtmClient();

            using (var splash = new SplashForm(client, config))
            {
                splash.ShowDialog();
            }
            Application.Run(new MainForm(client, config));
        }
    }
}
