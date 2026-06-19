using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;

namespace InclusaoDiversidade.Application.Common.Interfaces;

public interface IDepartamentoService
{
    Task<PagedResult<DepartamentoDto>> ListarAsync(PaginationQuery paginacao, CancellationToken ct = default);
}
