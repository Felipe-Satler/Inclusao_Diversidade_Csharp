using System.Text.Json.Serialization;

namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>
/// Envelope padrão de resposta da API. Toda resposta de sucesso traz um indicador
/// (<see cref="Sucesso"/>), uma <see cref="Mensagem"/> contextual e os <see cref="Dados"/>.
/// Em listagens, os metadados de paginação vão aninhados em <see cref="Paginacao"/>.
/// </summary>
public class RespostaApi<T>
{
    public bool Sucesso { get; init; } = true;

    public string Mensagem { get; init; } = string.Empty;

    public T? Dados { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginacaoMetadados? Paginacao { get; init; }

    public static RespostaApi<T> Ok(T dados, string mensagem)
        => new() { Sucesso = true, Mensagem = mensagem, Dados = dados };

    public static RespostaApi<T> OkPaginado(T dados, PaginacaoMetadados paginacao, string mensagem)
        => new() { Sucesso = true, Mensagem = mensagem, Dados = dados, Paginacao = paginacao };
}
