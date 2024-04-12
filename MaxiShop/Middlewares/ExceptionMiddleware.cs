using MaxiShop.Application.Exceptions;
using MaxiShop.Models;
using System.Net;

namespace MaxiShop.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex )
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statuscode = HttpStatusCode.InternalServerError;

            CustomerProblemDetails problem = null;

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statuscode = HttpStatusCode.BadRequest;
                    problem = new CustomerProblemDetails()
                    {
                        Title = badRequestException.Message,
                        Status = (int?)statuscode,
                        Type = nameof(badRequestException),
                        Detail = badRequestException.InnerException?.Message,
                        Errors = badRequestException.validationErrors
                    };
                    break;
            }
            httpContext.Response.StatusCode = (int)statuscode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
