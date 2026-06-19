namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>Metadados de paginação devolvidos dentro do envelope de resposta.</summary>
public class PaginacaoMetadados
{
    public int Pagina { get; init; }
    public int Tamanho { get; init; }
    public int TotalItens { get; init; }
    public int TotalPaginas { get; init; }
    public bool TemProxima { get; init; }
    public bool TemAnterior { get; init; }

    public static PaginacaoMetadados De<T>(PagedResult<T> resultado) => new()
    {
        Pagina = resultado.Page,
        Tamanho = resultado.PageSize,
        TotalItens = resultado.TotalItems,
        TotalPaginas = resultado.TotalPages,
        TemProxima = resultado.HasNext,
        TemAnterior = resultado.HasPrevious
    };
}
