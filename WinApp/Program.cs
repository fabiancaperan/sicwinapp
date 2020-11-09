using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
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
            new core.UseCase.InitDatabase.InitDb().initDatabase();
            Application.Run(new login());
            //Application.Run(new chargeFile());
            //Application.Run(new login());
            //Application.Run(new DowloadFiles());
        }
    }
}
