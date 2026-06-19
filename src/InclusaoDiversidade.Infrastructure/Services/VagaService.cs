using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.ViewModels;
using InclusaoDiversidade.Domain;
using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusaoDiversidade.Infrastructure.Services;

public class VagaService : IVagaService
{
    private const decimal LimiarDiversidade = 0.10m; // 10% — mesmo limiar da Trigger 4
    private readonly AppDbContext _db;

    public VagaService(AppDbContext db) => _db = db;

    public async Task<VagaDto> AbrirAsync(AbrirVagaRequest request, CancellationToken ct = default)
    {
        var flag = request.FlagAfirmativa.ToUpperInvariant();

        var departamento = await _db.TbDepartamentos
            .FirstOrDefaultAsync(d => d.IdDep == request.FkDep, ct)
            ?? throw new KeyNotFoundException($"Departamento {request.FkDep} não encontrado.");

        // Espelha a regra da Trigger 4 (bloqueio de vaga não inclusiva), retornando
        // um 400 amigável antes mesmo de o gatilho do banco ser acionado.
        if (flag == "N" && (departamento.MetaDiversidade ?? 0m) < LimiarDiversidade)
        {
            throw new InvalidOperationException(
                $"Abertura bloqueada: o departamento '{departamento.NomeDep}' possui meta de " +
                $"{(departamento.MetaDiversidade ?? 0m) * 100:0.##}% (mínimo de {LimiarDiversidade * 100:0.##}%). " +
                "Abra a vaga como afirmativa ('S') ou solicite revisão ao comitê de D&I.");
        }

        var proximoId = (await _db.TbVagas.MaxAsync(v => (int?)v.IdVaga, ct) ?? 100) + 1;

        var vaga = new TbVaga
        {
            IdVaga = proximoId,
            Cargo = request.Cargo,
            FkDep = request.FkDep,
            FlagAfirmativa = flag,
            Status = "ABERTA"
        };

        _db.TbVagas.Add(vaga);
        await _db.SaveChangesAsync(ct);

        return new VagaDto(vaga.IdVaga, vaga.Cargo, vaga.Status, vaga.FlagAfirmativa, vaga.FkDep);
    }

    public async Task<VagaDto> AtualizarStatusAsync(int idVaga, AtualizarStatusVagaRequest request, CancellationToken ct = default)
    {
        var status = request.Status.ToUpperInvariant();
        var permitidos = new[] { "ABERTA", "PREENCHIDA", "CANCELADA" };
        if (!permitidos.Contains(status))
            throw new ArgumentException($"Status inválido. Use um de: {string.Join(", ", permitidos)}.");

        var vaga = await _db.TbVagas.FirstOrDefaultAsync(v => v.IdVaga == idVaga, ct)
            ?? throw new KeyNotFoundException($"Vaga {idVaga} não encontrada.");

        // Transação explícita: se as Triggers 1 (contratação) e 3 (matrícula em
        // compliance) falharem no banco, a mudança de status é revertida.
        await using var transacao = await _db.Database.BeginTransactionAsync(ct);
        try
        {
            vaga.Status = status;
            await _db.SaveChangesAsync(ct);
            await transacao.CommitAsync(ct);
        }
        catch
        {
            await transacao.RollbackAsync(ct);
            throw;
        }

        return new VagaDto(vaga.IdVaga, vaga.Cargo, vaga.Status, vaga.FlagAfirmativa, vaga.FkDep);
    }
}
