using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApi.Filters
{
    public class SampleActionFilter : IActionFilter
    {
        private ILogger<SampleActionFilter> _logger;

        public SampleActionFilter(ILogger<SampleActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            _logger.LogInformation("END");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            _logger.LogInformation("BEGIN");
        }
    }
}