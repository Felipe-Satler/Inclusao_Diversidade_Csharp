using System.Net;
using System.Text.Json;
using InclusaoDiversidade.Application.Common.Models;

namespace InclusaoDiversidade.Api.Middleware;

/// <summary>
/// Middleware global de tratamento de exceções. Converte exceções não tratadas
/// em respostas JSON padronizadas (sucesso=false + mensagem) com o código HTTP
/// adequado (400/403/404/500).
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
        // Erros de regra de negócio das triggers (RAISE_APPLICATION_ERROR -20001/-20002)
        // chegam encapsulados em DbUpdateException/OracleException → tratamos como 400.
        var erroTrigger = ExtrairErroDeTrigger(exception);

        var statusCode = erroTrigger is not null
            ? HttpStatusCode.BadRequest
            : exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,          // 404
                UnauthorizedAccessException => HttpStatusCode.Forbidden,  // 403
                ArgumentException => HttpStatusCode.BadRequest,           // 400
                InvalidOperationException => HttpStatusCode.BadRequest,   // 400
                _ => HttpStatusCode.InternalServerError                   // 500
            };

        var mensagem = erroTrigger
            ?? (statusCode == HttpStatusCode.InternalServerError
                ? "Ocorreu um erro inesperado ao processar a requisição."
                : exception.Message);

        var error = new ErrorResponse
        {
            Sucesso = false,
            Mensagem = mensagem,
            StatusCode = (int)statusCode,
            Path = context.Request.Path
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(error, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Percorre a cadeia de exceções procurando uma mensagem de erro de aplicação
    /// das triggers Oracle (ORA-20001/ORA-20002). Independente do provider (busca textual).
    /// </summary>
    private static string? ExtrairErroDeTrigger(Exception exception)
    {
        for (Exception? e = exception; e is not null; e = e.InnerException)
        {
            if (e.Message.Contains("ORA-20001", StringComparison.OrdinalIgnoreCase) ||
                e.Message.Contains("ORA-20002", StringComparison.OrdinalIgnoreCase))
            {
                return e.Message;
            }
        }

        return null;
    }
}
