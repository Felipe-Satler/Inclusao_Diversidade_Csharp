using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InclusaoDiversidade.Domain;
using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;

namespace InclusaoDiversidade.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Mantive comentado para facilitar o seu primeiro teste sem precisar do Token
public class CandidatosController : ControllerBase
{
    private readonly AppDbContext _context;

    // Injeta o banco de dados que configuramos no Program.cs
    public CandidatosController(AppDbContext context)
    {
        _context = context;
    }

    // --------------------------------------------------------
    // ROTA GET: Buscar todos os candidatos (Teste de Leitura)
    // --------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> GetCandidatos()
    {
        var candidatos = await _context.TbCandidatos.ToListAsync();
        return Ok(candidatos);
    }

    // --------------------------------------------------------
    // ROTA POST: Inserir novo candidato (Teste da Trigger)
    // --------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> PostCandidato([FromBody] TbCandidato candidato)
    {
        _context.TbCandidatos.Add(candidato);
        
        // É neste momento que o C# envia o comando pro Oracle e a sua Trigger 2 é ativada!
        await _context.SaveChangesAsync(); 
        
        return CreatedAtAction(nameof(GetCandidatos), new { id = candidato.IdCandidato }, candidato);
    }
}