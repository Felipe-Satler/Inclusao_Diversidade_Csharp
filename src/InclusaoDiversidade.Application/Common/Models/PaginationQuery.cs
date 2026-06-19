namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>
/// Parâmetros de paginação recebidos via query string (ex.: ?pagina=1&amp;tamanho=10).
/// Expõe <see cref="Skip"/>/<see cref="Take"/> prontos para usar no EF Core.
/// </summary>
public class PaginationQuery
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;

    private int _pageSize = DefaultPageSize;
    private int _page = 1;

    /// <summary>Número da página (base 1).</summary>
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    /// <summary>Tamanho da página, limitado a <see cref="MaxPageSize"/>.</summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }

    public int Skip => (Page - 1) * PageSize;

    public int Take => PageSize;
}
