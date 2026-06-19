using System.Net;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

public class ColaboradoresControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public ColaboradoresControllerTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Get_Treinamentos_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/colaboradores/treinamentos?pagina=1&tamanho=10");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
