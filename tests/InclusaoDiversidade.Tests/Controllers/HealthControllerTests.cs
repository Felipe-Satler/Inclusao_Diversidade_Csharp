using System.Net;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

/// <summary>
/// Teste de integração modelo. Cada controller da aplicação deve ter pelo menos
/// um teste neste formato, validando que o endpoint retorna status 200.
/// </summary>
public class HealthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HealthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Health_ReturnsHttpStatusCode200()
    {
        // Arrange
        var request = "/health";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        response.EnsureSuccessStatusCode(); // Verifica se o status code é 200
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
