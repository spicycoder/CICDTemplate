using FluentAssertions;

using System.Net;

namespace CICDTemplate.AspireTests;

public class AppHostTest
{
    [Fact]
    public async Task AppHost_ShouldLoad_ApiAndDatabase()
    {
        // arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.CICDTemplate_AppHost>();
        DistributedApplication app = await appHost.BuildAsync();
        await app.StartAsync();
        var client = app.CreateHttpClient("cicdtemplate-api");

        // act
        var response = await client.GetAsync(new Uri("/api/products", UriKind.Relative));

        client.Dispose();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
