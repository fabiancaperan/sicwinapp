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
            //try
            //{
            //    // Start!
            //    MainAsync().Wait();
            //    //return 0;
            //}
            //catch
            //{
            //    //return 1;
            //}
        }
        //static async Task MainAsync()
        //{
        //    // Create service collection
            
        //    ServiceCollection serviceCollection = new ServiceCollection();
        //    ConfigureServices(serviceCollection);

        //    // Create service provider
            
        //    IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            

          
        //}

        //private static void ConfigureServices(IServiceCollection serviceCollection)
        //{
        //    // Add logging
        //    serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
        //    {
              
        //    }));

        //    serviceCollection.AddLogging();

        //    // Build configuration
        //    var builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //    IConfigurationRoot configuration = builder.Build();

        //    // Add access to generic IConfigurationRoot
        //    serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        //    var test = configuration;


        //}
    }

}
