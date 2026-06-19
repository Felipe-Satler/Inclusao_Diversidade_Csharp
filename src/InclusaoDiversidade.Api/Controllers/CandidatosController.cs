using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidatosController : ControllerBase
{
    private readonly ICandidatoService _service;
    public CandidatosController(ICandidatoService service) => _service = service;

    /// <summary>
    /// Lista todos os candidatos (paginado), ordenados por Score de Diversidade (desc).
    /// Para inscrever um candidato em uma vaga, use POST /vagas/{id}/candidatos.
    /// </summary>
    /// <remarks>GET /candidatos?pagina=1&amp;tamanho=10</remarks>
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery(Name = "pagina")] int pagina = 1,
        [FromQuery(Name = "tamanho")] int tamanho = 10,
        CancellationToken ct = default)
    {
        var paginacao = new PaginationQuery { Page = pagina, PageSize = tamanho };
        var resultado = await _service.ListarTodosAsync(paginacao, ct);

        return Ok(new RespostaCandidatos
        {
            Mensagem = "Lista de candidatos resgatada com sucesso.",
            Candidatos = resultado.Items,
            Paginacao = PaginacaoMetadados.De(resultado)
        });
    }
}
