using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApi.Middleware
{
    public class FactorySampleMiddleware : IMiddleware
    {
        private ILogger<FactorySampleMiddleware> _logger;

        public FactorySampleMiddleware(ILogger<FactorySampleMiddleware> logger)
        {
            _logger = logger;
        }

        private int counter;
        private int limit = 5;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(counter >= limit)
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            else
            {
            counter++;
            try {
                _logger.LogInformation("BEGIN");
                await next(context);
                _logger.LogInformation("END");
            }
            finally {
                counter--;
            }


            }
        }
    }
}