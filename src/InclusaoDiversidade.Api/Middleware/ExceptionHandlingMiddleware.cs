using System.Net;
using System.Text.Json;
using InclusaoDiversidade.Application.Common.Models;

namespace InclusaoDiversidade.Api.Middleware;

/// <summary>
/// Middleware global de tratamento de exceções. Converte exceções não tratadas
/// em respostas JSON padronizadas com o código HTTP adequado (400/403/404/500).
/// </summary>
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado ao processar {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,          // 404
            UnauthorizedAccessException => HttpStatusCode.Forbidden,  // 403
            ArgumentException => HttpStatusCode.BadRequest,           // 400
            InvalidOperationException => HttpStatusCode.BadRequest,   // 400
            _ => HttpStatusCode.InternalServerError                   // 500
        };

        var error = new ErrorResponse((int)statusCode, exception.Message, context.Request.Path);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(error, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
