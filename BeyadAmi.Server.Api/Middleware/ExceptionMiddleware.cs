using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BeyadAmi.Server.Application.Exceptions;

namespace BeyadAmi.Server.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred while processing request");

                var traceId = context.TraceIdentifier;
                int statusCode;
                string message;

                // Map special application exceptions
                var exTypeName = ex.GetType().Name;

                if (ex is BranchNotFoundException || exTypeName.EndsWith("NotFoundException"))
                {
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = ex.Message;
                }
                else if (ex is BusinessException || exTypeName.Contains("AlreadyExists") || ex is BranchHasDevicesException)
                {
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                }
                else
                {
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                }

                var result = JsonSerializer.Serialize(new { message, statusCode, traceId });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
