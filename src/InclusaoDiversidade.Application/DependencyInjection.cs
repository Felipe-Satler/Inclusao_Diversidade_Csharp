using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace InclusaoDiversidade.Application;

/// <summary>
/// Registro de dependências da camada de Aplicação.
/// Chamado a partir do <c>Program.cs</c> via <c>builder.Services.AddApplication()</c>.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra automaticamente todos os validators (FluentValidation)
        // declarados nesta assembly. Hoje não há nenhum; serão adicionados
        // junto com os ViewModels dos endpoints.
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // TODO: registrar serviços de aplicação (ex.: services.AddScoped<IVagaService, VagaService>()).

        return services;
    }
}
