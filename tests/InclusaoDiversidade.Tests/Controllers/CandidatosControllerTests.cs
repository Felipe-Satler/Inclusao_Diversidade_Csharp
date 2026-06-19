using System.Net;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

public class CandidatosControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public CandidatosControllerTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Get_Candidatos_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/candidatos?pagina=1&tamanho=10");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
