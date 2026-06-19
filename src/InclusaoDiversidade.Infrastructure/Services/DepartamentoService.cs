using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusaoDiversidade.Infrastructure.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly AppDbContext _db;
    public DepartamentoService(AppDbContext db) => _db = db;

    public async Task<PagedResult<DepartamentoDto>> ListarAsync(PaginationQuery p, CancellationToken ct = default)
    {
        var query = _db.TbDepartamentos.AsNoTracking().OrderBy(d => d.IdDep);

        var total = await query.CountAsync(ct);

        var itens = await query
            .Skip(p.Skip)
            .Take(p.Take)
            .Select(d => new DepartamentoDto(
                d.IdDep,
                d.NomeDep,
                d.MetaDiversidade,
                d.MetaDiversidade * 100))
            .ToListAsync(ct);

        return new PagedResult<DepartamentoDto>(itens, total, p.Page, p.PageSize);
    }
}
