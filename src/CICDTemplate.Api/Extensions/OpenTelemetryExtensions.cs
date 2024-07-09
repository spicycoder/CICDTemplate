using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CICDTemplate.Api.Extensions;

public static class OpenTelemetryExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenTelemetry(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        string zipkinUri = builder.Configuration.GetSection("Telemetry:Zipkin").Value!;

        builder
            .Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("CICDTemplate.Api"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddZipkinExporter(config => config.Endpoint = new Uri(zipkinUri, UriKind.Absolute));

                tracing.AddOtlpExporter();
            });

        builder
            .Logging
            .AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.IncludeScopes = true;

                logging.AddOtlpExporter();
            });

        return builder;
    }
}
