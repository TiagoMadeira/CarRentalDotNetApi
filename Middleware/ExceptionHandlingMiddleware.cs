using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlingMiddleware> logger)

        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException httpException)
            {
                _logger.LogError(httpException, "Error occured: {Message}", httpException.Message);

                var details = new ProblemDetails();

                details.Title = "Error Occured";
                details.Status = (int)httpException.StatusCode;
                details.Extensions.Add("Error",httpException.Message);

                context.Response.StatusCode = (int)httpException.StatusCode;
                await context.Response.WriteAsJsonAsync(details);

            }
            catch (Exception exception){

                 _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

                var details = new ProblemDetails();

                details.Title = "Exception Occured";
                details.Status = StatusCodes.Status500InternalServerError;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
}