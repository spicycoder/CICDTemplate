namespace CICDTemplate.Api.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected HttpClient Client { get; private set; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory), "Factory parameter cannot be null.");
        }

        Client = factory.CreateClient();
    }
}
