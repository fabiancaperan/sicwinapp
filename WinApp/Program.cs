using System;
using System.Windows.Forms;

namespace WinApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new core.UseCase.InitDatabase.InitDb().InitDatabase();
            Application.Run(new Login());
            //Application.Run(new chargeFile());
            //Application.Run(new login());
            //Application.Run(new DowloadFiles());
        }
    }
}
