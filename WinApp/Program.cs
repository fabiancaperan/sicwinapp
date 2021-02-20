using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace WinApp
{
    public static class Program
    {
        public static bool IsAdmin = false;
        public static string UserName = String.Empty;
        private static Logger _log;

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

            using var serviceScope = host.Services.CreateScope();
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var login = services.GetRequiredService<Login>();
                    _log = LogManager.GetLogger(nameof(Program));
                    Application.Run(login);
                    var success = "Success";
                    Console.WriteLine(success);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    var errorOccured = "Error Occured";
                    Console.WriteLine(errorOccured);
                }

            }
        }

        public static void LogError(Exception ex) => _log.Log<Exception>(NLog.LogLevel.Error, UserName + ";" + ex.Message + ex.StackTrace, ex);
        public static void LogInfo(string msj) => _log.Log(NLog.LogLevel.Info, UserName + ";" + msj);
    }

}
