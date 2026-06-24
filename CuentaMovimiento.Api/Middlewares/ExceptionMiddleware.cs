using System.Net;
using System.Text.Json;
using CuentaMovimiento.Api.Exceptions;

namespace CuentaMovimiento.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteError(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (BusinessException ex)
            {
                await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch
            {
                await WriteError(context, HttpStatusCode.InternalServerError, "Ocurrio un error inesperado.");
            }
        }

        private static async Task WriteError(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                message
            });

            await context.Response.WriteAsync(response);
        }
    }
}
