using InclusaoDiversidade.Application.ViewModels;

namespace InclusaoDiversidade.Application.Common.Interfaces;

public interface IVagaService
{
    Task<VagaDto> AbrirAsync(AbrirVagaRequest request, CancellationToken ct = default);
    Task<VagaDto> AtualizarStatusAsync(int idVaga, AtualizarStatusVagaRequest request, CancellationToken ct = default);
}
