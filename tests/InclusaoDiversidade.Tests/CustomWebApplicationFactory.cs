using InclusaoDiversidade.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InclusaoDiversidade.Tests;

/// <summary>
/// Fábrica do host de testes de integração. Sobe a API em memória, injeta a
/// configuração mínima de JWT e troca o provider Oracle por EF Core InMemory,
/// permitindo que os testes de status 200 rodem sem um banco real.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                // Connection string fictícia só para o startup não falhar;
                // ela é descartada logo abaixo ao trocar o provider por InMemory.
                ["ConnectionStrings:OracleConnection"] = "Data Source=tests;User Id=tests;Password=tests;",
                ["Jwt:Issuer"] = "InclusaoDiversidade.Tests",
                ["Jwt:Audience"] = "InclusaoDiversidade.Tests",
                ["Jwt:SecretKey"] = "chave-de-teste-super-secreta-com-mais-de-32-caracteres!!",
                ["Jwt:ExpirationMinutes"] = "60"
            });
        });

        builder.ConfigureServices(services =>
        {
            // Remove o registro do DbContext Oracle...
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor is not null)
                services.Remove(descriptor);

            // ...e substitui por um banco em memória para os testes.
            services.AddDbContext<AppDbContext>(options =>
                options
                    .UseInMemoryDatabase("InclusaoDiversidadeTests")
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        });
    }
}
