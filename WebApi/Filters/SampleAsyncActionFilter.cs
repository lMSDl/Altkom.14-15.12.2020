using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApi.Filters
{
    public class SampleAsyncActionFilter : IAsyncActionFilter
    {        
        private ILogger<SampleAsyncActionFilter> _logger;

        public SampleAsyncActionFilter(ILogger<SampleAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            _logger.LogInformation("BEGIN");
            
            await next();
            _logger.LogInformation("END");

        }
    }
}