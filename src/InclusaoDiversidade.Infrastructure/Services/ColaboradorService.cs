using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using InclusaoDiversidade.Domain;
using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusaoDiversidade.Infrastructure.Services;

public class ColaboradorService : IColaboradorService
{
    private readonly AppDbContext _db;
    public ColaboradorService(AppDbContext db) => _db = db;

    public async Task<PagedResult<ColaboradorTreinamentoDto>> ListarComTreinamentosAsync(
        PaginationQuery p, string? status = null, CancellationToken ct = default)
    {
        var query = _db.TbColaboradores
            .AsNoTracking()
            .Include(c => c.TbTreinamentosLogs)
            .OrderBy(c => c.IdColab);

        var total = await query.CountAsync(ct);

        var colaboradores = await query
            .Skip(p.Skip)
            .Take(p.Take)
            .ToListAsync(ct);

        var filtro = status?.Trim().ToLowerInvariant();

        var itens = colaboradores.Select(c =>
        {
            IEnumerable<TbTreinamentosLog> logs = c.TbTreinamentosLogs;

            if (filtro == "concluido")
                logs = logs.Where(t => t.DataConclusao != null);
            else if (filtro == "pendente")
                logs = logs.Where(t => t.DataConclusao == null);

            var treinamentos = logs
                .Select(t => new TreinamentoItemDto(
                    t.TipoTreinamento,
                    t.DataConclusao,
                    t.DataConclusao == null ? "Pendente" : "Concluído"))
                .ToList();

            return new ColaboradorTreinamentoDto(c.IdColab, c.Nome, c.FkDep, treinamentos);
        }).ToList();

        return new PagedResult<ColaboradorTreinamentoDto>(itens, total, p.Page, p.PageSize);
    }
}
