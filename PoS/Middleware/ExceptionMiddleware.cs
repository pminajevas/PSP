using Newtonsoft.Json;
using PoS.Core.Exceptions;

namespace PoS.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (PoSException e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)e.StatusCode;

                await httpContext.Response.WriteAsync($"{JsonConvert.SerializeObject(new { Message = e.Message })}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.StackTrace);

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
