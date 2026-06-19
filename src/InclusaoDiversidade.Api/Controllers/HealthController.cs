using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

/// <summary>
/// Endpoint de verificação de saúde da API (público, sem autenticação).
/// Serve como modelo de controller e é validado pelo teste xUnit de status 200.
/// </summary>
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
}
