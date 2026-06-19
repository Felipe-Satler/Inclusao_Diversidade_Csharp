using InclusaoDiversidade.Api.Extensions;
using InclusaoDiversidade.Api.Middleware;
using InclusaoDiversidade.Application;
using InclusaoDiversidade.Application.Common.Models;
using InclusaoDiversidade.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Composição das camadas
//   AddApplication    → validators e serviços de aplicação
//   AddInfrastructure → DbContext (Oracle), serviços de negócio e JWT
// ---------------------------------------------------------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Erros de validação (400) no mesmo formato padronizado da API
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var erros = context.ModelState
            .Where(kvp => kvp.Value is not null && kvp.Value.Errors.Count > 0)
            .SelectMany(kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage))
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .ToList();

        var resposta = new ErrorResponse
        {
            Sucesso = false,
            Mensagem = erros.Count > 0
                ? string.Join(" ", erros)
                : "Requisição inválida: verifique os dados enviados.",
            StatusCode = StatusCodes.Status400BadRequest,
            Path = context.HttpContext.Request.Path
        };

        return new BadRequestObjectResult(resposta);
    };
});

// Autenticação/Autorização (JWT) e Swagger com suporte a Bearer
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

// ---------------------------------------------------------------------------
// Pipeline HTTP
// ---------------------------------------------------------------------------

// Tratamento global de exceções (cedo no pipeline)
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger sempre habilitado: a aplicação abre direto na documentação.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// A raiz redireciona para o Swagger (ex.: https://localhost:5001 -> /swagger).
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

// Exposto como partial public para que os testes de integração possam
// referenciar o tipo Program em WebApplicationFactory<Program>.
public partial class Program { }
