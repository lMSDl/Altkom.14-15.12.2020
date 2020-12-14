using System.Linq;
using Console.Models;
using Console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Console
{
    class Program
    {
        static Settings Settings {get;} = new Settings();
        static ServiceProvider ServiceProvider {get; set;}

        static void Main(string[] args)
        {
            Configuration();

            var serviceCollection = new ServiceCollection();
            ServiceProvider = serviceCollection
            .AddScoped<IWriteService, ConsoleWriteService>()
            .AddScoped<IFiggleWriteService, FiggleWriteService>()
            .BuildServiceProvider();


            Write();

        }

        static public void Write() {
            var services =  ServiceProvider.GetServices<IFiggleWriteService>();
            //Hello(config["Section:Key2"], config["Section:Subsection:Key1"]);

            foreach(var service in services )
                Hello(service, Settings.Section.Key1, Settings.Section.Subsection.Key1);
        }

        private static void Configuration()
        {
            var config = new ConfigurationBuilder()
            //.AddXmlFile("configapp.xml", optional: false, reloadOnChange: true)
            //.AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            //.AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            .Build();
            config.Bind(Settings);
        }

        private static void Hello(IWriteService service, string hello, string from)
        {
            service.WriteLine($"{hello} {from}!");
        }
    }
}
