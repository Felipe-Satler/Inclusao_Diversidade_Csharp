namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>
/// Formato padronizado de resposta de erro. Mantém os mesmos campos de contexto
/// do envelope de sucesso (<c>sucesso</c> + <c>mensagem</c>) para o cliente tratar
/// sucesso e erro de forma uniforme.
/// </summary>
public record ErrorResponse
{
    public bool Sucesso { get; init; }
    public string Mensagem { get; init; } = string.Empty;
    public int StatusCode { get; init; }
    public string? Path { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
