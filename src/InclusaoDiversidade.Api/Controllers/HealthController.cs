using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

/// <summary>
/// Endpoint de verificação de saúde da API (público, sem autenticação).
/// </summary>
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => Ok(new
        {
            sucesso = true,
            mensagem = "API operacional.",
            status = "Healthy",
            timestamp = DateTime.UtcNow
        });
}
