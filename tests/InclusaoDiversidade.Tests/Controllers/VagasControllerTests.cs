using System.Net;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

public class VagasControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public VagasControllerTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Get_CandidatosDaVaga_ReturnsHttpStatusCode200()
    {
        // Endpoint público de leitura (a vaga não precisa existir: retorna lista vazia).
        var response = await _client.GetAsync("/vagas/101/candidatos?pagina=1&tamanho=10");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
