using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApi.Filters
{
    public class ValidatorAsyncActionFilter : IAsyncActionFilter
    {        
        private ILogger<ValidatorAsyncActionFilter> _logger;

        public ValidatorAsyncActionFilter(ILogger<ValidatorAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;
            if(!modelState.IsValid) {
                var errors = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(string.Join("\n -", errors));
            }
            else
                await next();
        }
    }
}