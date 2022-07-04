using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WebAPI.APIErrors;

namespace WebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger logger;
        private readonly IHostEnvironment env;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger, IHostEnvironment env)
        {
            this._next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ApiError apiError;
                //HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                string message = null;
                int statusCode = (int)HttpStatusCode.InternalServerError;
                var exceptionType = ex.GetType();
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    message = "You are not authorized";
                    statusCode = (int)HttpStatusCode.Forbidden;
                }
                else
                {
                    message = "We are working in the backend please wait and try after sometime!";
                    statusCode = (int)HttpStatusCode.InternalServerError;
                }

                if (env.IsDevelopment())
                {
                    apiError = new ApiError(statusCode, ex.Message, ex.StackTrace.ToString());
                }
                else
                {
                    apiError = new ApiError(statusCode, message);
                }

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsync(apiError.ToString());
            }
        }

    }
}
