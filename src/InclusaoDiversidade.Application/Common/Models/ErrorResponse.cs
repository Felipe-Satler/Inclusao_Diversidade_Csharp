namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>
/// Formato padronizado de resposta de erro retornado pelo middleware global
/// de tratamento de exceções.
/// </summary>
public record ErrorResponse(int StatusCode, string Message, string? Path = null)
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
