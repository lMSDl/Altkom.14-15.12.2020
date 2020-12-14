using System.Linq;
using Console.Models;
using Console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Console
{
    class Program
    {
        static Settings Settings {get;} = new Settings();
        static ServiceProvider ServiceProvider {get; set;}

        static void Main(string[] args)
        {
            var config = Configuration();

            var serviceCollection = new ServiceCollection();
            ServiceProvider = serviceCollection
            .AddScoped<IWriteService, ConsoleWriteService>()
            .AddScoped<IFiggleWriteService, FiggleWriteService>()
            //.AddLogging(builder => builder.AddConsole().AddDebug()).Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
            .AddLogging(builder => builder.AddConsole().AddDebug().AddConfiguration(config.GetSection("Logging")))
            .BuildServiceProvider();


            Write();

        }

        static public void Write() {
            var services =  ServiceProvider.GetServices<IFiggleWriteService>();
            //Hello(config["Section:Key2"], config["Section:Subsection:Key1"]);
            var logger = ServiceProvider.GetService<ILogger<Program>>();
            foreach(var service in services ) {
                using(logger.BeginScope($"foreach for {service.GetType().Name}")) {
                    logger.LogDebug($"start");                
                    Hello(service, Settings.Section.Key1, Settings.Section.Subsection.Key1);
                    logger.LogInformation($"end");
                }
            }
        }

        private static IConfigurationRoot Configuration()
        {
            var config = new ConfigurationBuilder()
            //.AddXmlFile("configapp.xml", optional: false, reloadOnChange: true)
            //.AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            //.AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            .Build();
            config.Bind(Settings);
            return config;
        }

        private static void Hello(IWriteService service, string hello, string from)
        {
            service.WriteLine($"{hello} {from}!");
        }
    }
}
