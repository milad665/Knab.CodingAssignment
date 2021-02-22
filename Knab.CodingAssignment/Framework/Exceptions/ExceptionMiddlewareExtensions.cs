using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Knab.CodingAssignment.Framework.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var ex = contextFeature.Error;

                        var errorResponse = ex is IApplicationException
                            ? new ErrorResponseDto(ex.HResult, ex.Message)
                            : new ErrorResponseDto(0, "Oops! Something went wrong.");

                        await context.Response.WriteAsync(errorResponse.ToString()).ConfigureAwait(false);
                    }
                });
            });
        }
    }
}
