using ApiTemplate.Api.ViewModels;
using ApiTemplate.Domain.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace ApiTemplate.Api.Middlewares
{
    //Global Exception Handler
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation Model Exception: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest,string.Join(',',ex.Errors.Select(s=>s.ErrorMessage).ToList()));
            }
            catch (BusinessException ex)
            {
                _logger.LogError($"Bussiness Exception: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error: {ex}");
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;           
                      
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }));
        }
    }
}
