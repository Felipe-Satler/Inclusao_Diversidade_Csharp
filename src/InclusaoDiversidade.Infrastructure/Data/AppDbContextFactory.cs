using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InclusaoDiversidade.Infrastructure.Data;

/// <summary>
/// Fábrica usada APENAS em tempo de design pela ferramenta <c>dotnet ef</c>
/// (ex.: <c>migrations add</c>, <c>database update</c>).
///
/// Com o hosting mínimo (Program.cs com top-level statements), o <c>dotnet ef</c>
/// nem sempre consegue instanciar o <see cref="AppDbContext"/> a partir do host.
/// Esta fábrica garante isso sem precisar subir a API.
///
/// A connection string vem (nesta ordem) da variável de ambiente
/// <c>ConnectionStrings__OracleConnection</c> ou, na ausência dela, da string
/// padrão do projeto. Observação: <c>migrations add</c> NÃO conecta no banco —
/// só monta o modelo; a connection string só é usada de fato no <c>database update</c>.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__OracleConnection")
            ?? "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=RM561859;Password=589631;";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseOracle(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
