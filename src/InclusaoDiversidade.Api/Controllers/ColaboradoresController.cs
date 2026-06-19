using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ColaboradoresController : ControllerBase
{
    private readonly IColaboradorService _service;
    public ColaboradoresController(IColaboradorService service) => _service = service;

    /// <summary>
    /// Lista colaboradores e seus treinamentos obrigatórios (paginado).
    /// Filtro opcional por status: "concluido" ou "pendente".
    /// </summary>
    /// <remarks>GET /colaboradores/treinamentos?pagina=1&amp;tamanho=10&amp;status=pendente</remarks>
    [HttpGet("treinamentos")]
    public async Task<IActionResult> ListarTreinamentos(
        [FromQuery(Name = "pagina")] int pagina = 1,
        [FromQuery(Name = "tamanho")] int tamanho = 10,
        [FromQuery(Name = "status")] string? status = null,
        CancellationToken ct = default)
    {
        var paginacao = new PaginationQuery { Page = pagina, PageSize = tamanho };
        var resultado = await _service.ListarComTreinamentosAsync(paginacao, status, ct);

        return Ok(new RespostaColaboradores
        {
            Mensagem = "Lista de treinamentos dos colaboradores resgatada com sucesso.",
            Colaboradores = resultado.Items,
            Paginacao = PaginacaoMetadados.De(resultado)
        });
    }
}
