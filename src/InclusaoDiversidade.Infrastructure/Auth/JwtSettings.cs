namespace InclusaoDiversidade.Infrastructure.Auth;

/// <summary>
/// Opções de configuração do JWT, vinculadas à seção "Jwt" do appsettings.json.
/// </summary>
public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 60;
}
