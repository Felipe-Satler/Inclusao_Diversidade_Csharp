using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

/// <summary>
/// Emissão de token JWT. Implementação mínima apenas para viabilizar o teste
/// dos endpoints protegidos — credenciais fixas de demonstração.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(IJwtTokenService tokenService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public IActionResult GenerateToken([FromBody] LoginRequest request)
    {
        // Credenciais fixas, apenas para fins de demonstração/teste.
        if (request is { Username: "admin", Password: "admin123" })
        {
            var token = _tokenService.GenerateToken(request.Username, new[] { "Administrador" });
            var minutos = _configuration.GetValue<int?>("Jwt:ExpirationMinutes") ?? 60;

            return Ok(new RespostaToken
            {
                Mensagem = "Token gerado com sucesso.",
                Autenticacao = new TokenDto(
                    AccessToken: token,
                    TokenType: "Bearer",
                    Perfil: "Administrador",
                    ExpiraEm: DateTime.UtcNow.AddMinutes(minutos))
            });
        }

        // Credenciais inválidas → 401 (não autenticado). É o código HTTP correto:
        // 403 é reservado para usuário autenticado, porém sem permissão.
        var erro = new ErrorResponse
        {
            Sucesso = false,
            Mensagem = "Credenciais inválidas. Verifique o usuário e a senha informados.",
            StatusCode = StatusCodes.Status401Unauthorized,
            Path = Request.Path
        };
        return Unauthorized(erro);
    }

    public record LoginRequest(string Username, string Password);
}
