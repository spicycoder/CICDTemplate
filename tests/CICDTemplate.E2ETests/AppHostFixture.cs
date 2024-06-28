using Microsoft.Extensions.Hosting;

namespace CICDTemplate.E2ETests;

public class AppHostFixture : IDisposable
{
    private readonly DistributedApplication _app;

    protected internal HttpClient Client { get; set; }

    public AppHostFixture()
    {
        var appHost = DistributedApplicationTestingBuilder.CreateAsync<Projects.CICDTemplate_AppHost>().Result;
        _app = appHost.BuildAsync().Result;
        _app.Start();

        Client = _app.CreateHttpClient("cicdtemplate-api");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Client.Dispose();
        }

        _app.Dispose();
    }
}
