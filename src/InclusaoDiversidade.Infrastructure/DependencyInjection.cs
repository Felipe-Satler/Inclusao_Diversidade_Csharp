using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Infrastructure.Auth;
using InclusaoDiversidade.Infrastructure.Data;
using InclusaoDiversidade.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InclusaoDiversidade.Infrastructure;

/// <summary>
/// Registro de dependências da camada de Infraestrutura.
/// Chamado a partir do <c>Program.cs</c> via
/// <c>builder.Services.AddInfrastructure(builder.Configuration)</c>.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Banco de dados — Oracle (FIAP).
        // A connection string "OracleConnection" deve ser fornecida via
        // User Secrets (dev) ou variável de ambiente (Docker/produção).
        var connectionString = configuration.GetConnectionString("OracleConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'OracleConnection' não configurada. " +
                "Defina-a via 'dotnet user-secrets' ou na variável de ambiente " +
                "ConnectionStrings__OracleConnection.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(connectionString));

        // Serviços de negócio (implementam os contratos da camada de Aplicação)
        services.AddScoped<IDepartamentoService, DepartamentoService>();
        services.AddScoped<IVagaService, VagaService>();
        services.AddScoped<ICandidatoService, CandidatoService>();
        services.AddScoped<IColaboradorService, ColaboradorService>();

        // Autenticação: opções de JWT e serviço de geração de token
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
