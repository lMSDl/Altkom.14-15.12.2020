using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            //.AddXmlFile("configapp.xml", optional: false, reloadOnChange: true)
            //.AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            .//AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            .Build();

            Hello(config["Section:Key2"], config["Section:Subsection:Key1"]);
        }

        private static void Hello(string hello, string from)
        {
            System.Console.WriteLine(
                Figgle.FiggleFonts.Standard.Render(
                $"{hello} {from}!")
            );
        }
    }
}
