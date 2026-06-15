namespace InclusaoDiversidade.Application.Common.Interfaces;

/// <summary>
/// Contrato para geração de tokens JWT. A implementação concreta vive na
/// camada de Infraestrutura (<c>Infrastructure/Auth/JwtTokenService</c>).
/// </summary>
public interface IJwtTokenService
{
    string GenerateToken(string username, IEnumerable<string> roles);
}
