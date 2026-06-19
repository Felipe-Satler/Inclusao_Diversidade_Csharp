using Microsoft.EntityFrameworkCore;
using InclusaoDiversidade.Api.Extensions;
using InclusaoDiversidade.Api.Middleware;
using InclusaoDiversidade.Application;
using InclusaoDiversidade.Infrastructure;
using InclusaoDiversidade.Infrastructure.Data; // <-- Comentado temporariamente

var builder = WebApplication.CreateBuilder(args);

// Pega a string do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

// ---------------------------------------------------------------------------
// 🚨 ATENÇÃO: BLOCO COMENTADO TEMPORARIAMENTE PARA O SCAFFOLD RODAR
// Como apagamos o AppDbContext para o EF recriá-lo, o projeto precisa compilar sem ele primeiro.
// Descomente este bloco DEPOIS que o comando scaffold gerar as classes com sucesso!
// ---------------------------------------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));


// ---------------------------------------------------------------------------
// Registro de serviços (composição das camadas)
// ---------------------------------------------------------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Autenticação/Autorização (JWT) e Swagger com suporte a Bearer
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

// ---------------------------------------------------------------------------
// Pipeline HTTP
// ---------------------------------------------------------------------------

// Tratamento global de exceções (deve vir cedo no pipeline)
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Exposto como partial public para que os testes de integração possam
// referenciar o tipo Program em WebApplicationFactory<Program>.
public partial class Program { }