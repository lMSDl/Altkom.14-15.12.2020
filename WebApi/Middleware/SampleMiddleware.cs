using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApi.Middleware
{
    public class SampleMiddleware
    {
        private ILogger<SampleMiddleware> _logger;
        private RequestDelegate _next;

        public SampleMiddleware(RequestDelegate next, ILogger<SampleMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            
            _logger.LogInformation($"{DateTime.Now}: {context.Request.Host}\\{context.Request.Path} - StatusCode {context.Response.StatusCode}");
        }
    }
}