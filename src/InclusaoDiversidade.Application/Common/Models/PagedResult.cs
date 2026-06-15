namespace InclusaoDiversidade.Application.Common.Models;

/// <summary>
/// Envelope padrão para respostas paginadas. Inclui os metadados exigidos
/// pelo enunciado: totalItems, totalPages e hasNext.
/// </summary>
public class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;

    public PagedResult(IReadOnlyCollection<T> items, int totalItems, int page, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
        TotalPages = pageSize > 0
            ? (int)Math.Ceiling(totalItems / (double)pageSize)
            : 0;
    }
}
