using System.Net;

using FluentAssertions;

namespace CICDTemplate.E2ETests.Infrastructure;

[Collection("App Host")]
public class AppHostTest(AppHostFixture fixture)
{
    [Fact]
    public async Task ApiApplication_Should_Load()
    {
        // act
        var response = await fixture.Client.GetAsync(new Uri("/api/products", UriKind.Relative));

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}