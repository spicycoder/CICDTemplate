using System.Diagnostics;

namespace CICDTemplate.AppHost.Extensions
{
    internal static class ResourceBuilderExtensions
    {
        internal static IResourceBuilder<T> WithSwaggerUI<T>(this IResourceBuilder<T> builder)
            where T : IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs(
                "swagger-ui-docs",
                "Swagger API Documentation",
                "swagger");
        }

        internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
            where T : IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs(
                "scalar-docs",
                "Scalar API Documentation",
                "scalar/v1");
        }

        internal static IResourceBuilder<T> WithReDoc<T>(this IResourceBuilder<T> builder)
            where T : IResourceWithEndpoints
        {
            return builder.WithOpenApiDocs(
                "redoc-docs",
                "ReDoc API Documentation",
                "api-docs");
        }

        private static IResourceBuilder<T> WithOpenApiDocs<T>(
            this IResourceBuilder<T> builder,
            string name,
            string displayName,
            string openApiUiPath)
            where T : IResourceWithEndpoints
        {
            return builder.WithCommand(
                name,
                displayName,
                executeCommand: async _ =>
                {
                    try
                    {
                        var endpoint = builder.GetEndpoint("http");
                        var url = $"{endpoint.Url}/{openApiUiPath}";
                        Process.Start(new ProcessStartInfo(url)
                        {
                            UseShellExecute = true
                        });

                        return await Task.FromResult(new ExecuteCommandResult
                        {
                            Success = true
                        });
                    }
                    catch (InvalidOperationException ex)
                    {
                        return await Task.FromResult(new ExecuteCommandResult
                        {
                            Success = false,
                            ErrorMessage = ex.Message
                        });
                    }
                });
        }
    }
}