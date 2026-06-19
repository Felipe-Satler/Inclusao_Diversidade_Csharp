using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;

namespace InclusaoDiversidade.Application.Common.Interfaces;

public interface IColaboradorService
{
    Task<PagedResult<ColaboradorTreinamentoDto>> ListarComTreinamentosAsync(
        PaginationQuery paginacao, string? status = null, CancellationToken ct = default);
}
