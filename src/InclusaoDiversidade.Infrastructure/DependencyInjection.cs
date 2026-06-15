using InclusaoDiversidade.Application.Common.Interfaces;
using InclusaoDiversidade.Infrastructure.Auth;
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
        // =====================================================================
        // TODO (etapa de banco de dados): registrar o DbContext aqui.
        // O provider (Oracle ou SQL Server) será decidido posteriormente.
        //
        // Exemplo Oracle:
        //   services.AddDbContext<InclusaoDbContext>(opt =>
        //       opt.UseOracle(configuration.GetConnectionString("DefaultConnection")));
        //
        // Exemplo SQL Server:
        //   services.AddDbContext<InclusaoDbContext>(opt =>
        //       opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        // =====================================================================

        // Autenticação: vinculação das opções de JWT e serviço de geração de token
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // TODO: registrar repositórios concretos conforme as entidades forem criadas,
        // ex.: services.AddScoped<IVagaRepository, VagaRepository>();

        return services;
    }
}
