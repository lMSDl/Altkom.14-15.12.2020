using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(SampleActionFilter))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{summary:length(8)}")]
        //[ServiceFilter(typeof(SampleAsyncActionFilter))]
        public IEnumerable<WeatherForecast> Get([FromRoute]string summary)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .Where(x => x.Summary == summary)
            .ToArray();
        }

        [HttpGet("~/abc")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        
        [HttpGet("~/loop")]
        public Loop GetLoop() {
            var loop = new Loop();
            var loop2 = new Loop();
            loop.Parent = loop2;
            loop2.Parent = loop;
            return loop;
        }

        public class Loop {
            public Loop Parent {get; set;}
        }
    }
}
