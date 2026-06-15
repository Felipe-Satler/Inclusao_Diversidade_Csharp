using InclusaoDiversidade.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

/// <summary>
/// Emissão de token JWT. Implementação mínima apenas para viabilizar o teste
/// dos endpoints protegidos — NÃO é o foco da avaliação. Substitua por um fluxo
/// real (ex.: ASP.NET Identity) caso precise persistir usuários e papéis.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _tokenService;

    public AuthController(IJwtTokenService tokenService) => _tokenService = tokenService;

    [HttpPost("token")]
    [AllowAnonymous]
    public IActionResult GenerateToken([FromBody] LoginRequest request)
    {
        // Credenciais fixas, apenas para fins de demonstração/teste.
        if (request is { Username: "admin", Password: "admin123" })
        {
            var token = _tokenService.GenerateToken(request.Username, new[] { "Administrador" });
            return Ok(new { access_token = token, token_type = "Bearer" });
        }

        return Unauthorized(new { message = "Credenciais inválidas." });
    }

    public record LoginRequest(string Username, string Password);
}
