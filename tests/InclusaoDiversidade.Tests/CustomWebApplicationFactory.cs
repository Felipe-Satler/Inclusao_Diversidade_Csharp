using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace InclusaoDiversidade.Tests;

/// <summary>
/// Fábrica do host de testes de integração. Sobe a API em memória e injeta a
/// configuração mínima de JWT para que o startup conclua sem depender de
/// appsettings externos.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Issuer"] = "InclusaoDiversidade.Tests",
                ["Jwt:Audience"] = "InclusaoDiversidade.Tests",
                ["Jwt:SecretKey"] = "chave-de-teste-super-secreta-com-mais-de-32-caracteres!!",
                ["Jwt:ExpirationMinutes"] = "60"
            });
        });

        // =====================================================================
        // TODO (etapa de banco): quando o DbContext existir, substitua aqui o
        // provider real por EF Core InMemory para os testes rodarem sem banco:
        //
        // builder.ConfigureServices(services =>
        // {
        //     var descriptor = services.SingleOrDefault(
        //         d => d.ServiceType == typeof(DbContextOptions<InclusaoDbContext>));
        //     if (descriptor is not null) services.Remove(descriptor);
        //
        //     services.AddDbContext<InclusaoDbContext>(opt =>
        //         opt.UseInMemoryDatabase("InclusaoDiversidadeTests"));
        // });
        // =====================================================================
    }
}
