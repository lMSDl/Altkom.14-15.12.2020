using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddXmlFile("configapp.xml", optional: false, reloadOnChange: true)
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            .AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            .AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .Build();

            Hello(config["HelloJson"]);
            Hello(config["HelloIni"]);
            Hello(config["HelloXml"]);
            Hello(config["HelloYaml"]);
        }

        private static void Hello(string @string)
        {
            System.Console.WriteLine(
                Figgle.FiggleFonts.Standard.Render(
                $"Hello {@string}!")
            );
        }
    }
}
