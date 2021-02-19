using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace WinApp
{
    public class Program
    {
        public static bool isAdmin = false;
        public static string userName = String.Empty;
        public static Logger _log;

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
            //Application.Run(new Login());

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddScoped<Login>();
                   services.AddLogging(option =>
                   {
                       option.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
                       option.AddNLog("nlog.config");
                   });
               });
            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var login = services.GetRequiredService<Login>();
                    _log = LogManager.GetLogger(typeof(Program).Name);
                    Application.Run(login);
                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    logError(ex);
                    Console.WriteLine("Error Occured");
                }

            }
        }

        public static void logError(Exception ex) => _log.Log<Exception>(NLog.LogLevel.Error, userName + ";" + ex.Message + ex.StackTrace, ex);
        public static void logInfo(string msj) => _log.Log(NLog.LogLevel.Info, userName + ";" + msj);
    }

}
