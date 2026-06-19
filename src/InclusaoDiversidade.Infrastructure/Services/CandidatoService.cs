using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using InclusaoDiversidade.Domain;
using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusaoDiversidade.Infrastructure.Services;

public class CandidatoService : ICandidatoService
{
    private readonly AppDbContext _db;
    public CandidatoService(AppDbContext db) => _db = db;

    public async Task<CandidatoDto> RegistrarAsync(int idVaga, RegistrarCandidatoRequest request, CancellationToken ct = default)
    {
        var vagaExiste = await _db.TbVagas.AnyAsync(v => v.IdVaga == idVaga, ct);
        if (!vagaExiste)
            throw new KeyNotFoundException($"Vaga {idVaga} não encontrada.");

        var proximoId = (await _db.TbCandidatos.MaxAsync(c => (int?)c.IdCandidato, ct) ?? 500) + 1;

        var candidato = new TbCandidato
        {
            IdCandidato = proximoId,
            Nome = request.Nome,
            ScoreDiversidade = request.ScoreDiversidade,
            FkVaga = idVaga
        };

        _db.TbCandidatos.Add(candidato);
        await _db.SaveChangesAsync(ct);

        // A Trigger 2 pode ter bonificado o SCORE no banco (vaga afirmativa, +2 até 10).
        // Recarrega para devolver o valor realmente persistido.
        await _db.Entry(candidato).ReloadAsync(ct);

        return new CandidatoDto(candidato.IdCandidato, candidato.Nome, candidato.ScoreDiversidade, candidato.FkVaga);
    }

    public async Task<PagedResult<CandidatoDto>> ListarPorVagaAsync(int idVaga, PaginationQuery p, CancellationToken ct = default)
    {
        // Ordena por Score (desc): mesma ordem usada pela Trigger 1 (transparência).
        var query = _db.TbCandidatos.AsNoTracking()
            .Where(c => c.FkVaga == idVaga)
            .OrderByDescending(c => c.ScoreDiversidade);

        var total = await query.CountAsync(ct);

        var itens = await query
            .Skip(p.Skip)
            .Take(p.Take)
            .Select(c => new CandidatoDto(c.IdCandidato, c.Nome, c.ScoreDiversidade, c.FkVaga))
            .ToListAsync(ct);

        return new PagedResult<CandidatoDto>(itens, total, p.Page, p.PageSize);
    }
}
