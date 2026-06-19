using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace InclusaoDiversidade.Tests.Controllers;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public AuthControllerTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Post_Token_ComCredenciaisValidas_ReturnsHttpStatusCode200()
    {
        var response = await _client.PostAsJsonAsync(
            "/auth/token", new { username = "admin", password = "admin123" });

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
