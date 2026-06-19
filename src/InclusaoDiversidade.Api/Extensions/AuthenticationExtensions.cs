using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InclusaoDiversidade.Api.Extensions;

public static class AuthenticationExtensions
{
    /// <summary>
    /// Configura a autenticação JWT Bearer com base na seção "Jwt" do appsettings,
    /// incluindo respostas JSON contextuais para 401 (sem token) e 403 (sem permissão).
    /// </summary>
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"]
            ?? throw new InvalidOperationException(
                "Configuração 'Jwt:SecretKey' ausente no appsettings.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                // Mensagens contextuais de autorização (em vez de respostas vazias).
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse(); // evita o 401 vazio padrão
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var json = JsonSerializer.Serialize(new
                        {
                            sucesso = false,
                            mensagem = "Autenticação necessária. Gere um token em POST /auth/token " +
                                       "e envie no cabeçalho 'Authorization: Bearer {token}'.",
                            statusCode = StatusCodes.Status401Unauthorized,
                            path = context.Request.Path.Value
                        });

                        await context.Response.WriteAsync(json);
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var json = JsonSerializer.Serialize(new
                        {
                            sucesso = false,
                            mensagem = "Acesso negado: seu perfil não tem permissão " +
                                       "(necessário 'Administrador') para este recurso.",
                            statusCode = StatusCodes.Status403Forbidden,
                            path = context.Request.Path.Value
                        });

                        await context.Response.WriteAsync(json);
                    }
                };
            });

        return services;
    }
}
