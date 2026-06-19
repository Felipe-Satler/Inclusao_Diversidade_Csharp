using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartamentosController : ControllerBase
{
    private readonly IDepartamentoService _service;
    public DepartamentosController(IDepartamentoService service) => _service = service;

    /// <summary>
    /// Lista os departamentos e suas metas de diversidade (paginado). Endpoint público.
    /// </summary>
    /// <remarks>GET /departamentos?pagina=1&amp;tamanho=10</remarks>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery(Name = "pagina")] int pagina = 1,
        [FromQuery(Name = "tamanho")] int tamanho = 10,
        CancellationToken ct = default)
    {
        var paginacao = new PaginationQuery { Page = pagina, PageSize = tamanho };
        var resultado = await _service.ListarAsync(paginacao, ct);
        return Ok(resultado);
    }
}
