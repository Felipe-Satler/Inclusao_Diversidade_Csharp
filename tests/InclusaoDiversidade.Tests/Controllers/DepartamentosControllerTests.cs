using System.Net;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

public class DepartamentosControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public DepartamentosControllerTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Get_Departamentos_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/departamentos?pagina=1&tamanho=10");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
