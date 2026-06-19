using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VagasController : ControllerBase
{
    private readonly IVagaService _vagaService;
    private readonly ICandidatoService _candidatoService;

    public VagasController(IVagaService vagaService, ICandidatoService candidatoService)
    {
        _vagaService = vagaService;
        _candidatoService = candidatoService;
    }

    /// <summary>
    /// Abre uma nova vaga. A Trigger 4 bloqueia vagas não afirmativas em
    /// departamentos abaixo da meta mínima. Sensível: exige perfil Administrador.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Abrir([FromBody] AbrirVagaRequest request, CancellationToken ct)
    {
        var vaga = await _vagaService.AbrirAsync(request, ct);
        return CreatedAtAction(nameof(ListarCandidatos), new { id = vaga.Id }, vaga);
    }

    /// <summary>
    /// Atualiza o status de uma vaga (ex.: PREENCHIDA). Ao preencher, as Triggers 1
    /// (contratação por mérito) e 3 (matrícula em compliance) são acionadas no banco.
    /// Sensível: exige perfil Administrador.
    /// </summary>
    [HttpPatch("{id:int}/status")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AtualizarStatus(
        int id, [FromBody] AtualizarStatusVagaRequest request, CancellationToken ct)
    {
        var vaga = await _vagaService.AtualizarStatusAsync(id, request, ct);
        return Ok(vaga);
    }

    /// <summary>
    /// Lista os candidatos de uma vaga, ordenados por Score de Diversidade (desc).
    /// </summary>
    [HttpGet("{id:int}/candidatos")]
    public async Task<IActionResult> ListarCandidatos(
        int id,
        [FromQuery(Name = "pagina")] int pagina = 1,
        [FromQuery(Name = "tamanho")] int tamanho = 10,
        CancellationToken ct = default)
    {
        var paginacao = new PaginationQuery { Page = pagina, PageSize = tamanho };
        var resultado = await _candidatoService.ListarPorVagaAsync(id, paginacao, ct);
        return Ok(resultado);
    }

    /// <summary>
    /// Inscreve um candidato em uma vaga. A Trigger 2 bonifica em +2 (até 10) o
    /// score quando a vaga é afirmativa.
    /// </summary>
    [HttpPost("{id:int}/candidatos")]
    public async Task<IActionResult> RegistrarCandidato(
        int id, [FromBody] RegistrarCandidatoRequest request, CancellationToken ct)
    {
        var candidato = await _candidatoService.RegistrarAsync(id, request, ct);
        return CreatedAtAction(nameof(ListarCandidatos), new { id }, candidato);
    }
}
