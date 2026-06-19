using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;

namespace InclusaoDiversidade.Application.Common.Interfaces;

public interface ICandidatoService
{
    Task<CandidatoDto> RegistrarAsync(int idVaga, RegistrarCandidatoRequest request, CancellationToken ct = default);
    Task<PagedResult<CandidatoDto>> ListarPorVagaAsync(int idVaga, PaginationQuery paginacao, CancellationToken ct = default);
}
