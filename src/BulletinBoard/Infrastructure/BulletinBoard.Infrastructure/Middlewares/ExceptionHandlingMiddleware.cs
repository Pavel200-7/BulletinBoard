using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Contracts.Errors.Exeptions;

namespace BulletinBoard.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (ValidationExeption e)
            {
                //_logger.LogError(e, "Что-то пошло не так");
                await HandleValidatioExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Что-то пошло не так");
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleValidatioExceptionAsync(HttpContext context, ValidationExeption exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ValidationErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Errors = exception.ValidationErrors,
                TraceId = context.TraceIdentifier
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Что-то пошло не так. Попробуйте позже.",
                TraceId = context.TraceIdentifier
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
