using System.Diagnostics.Metrics;

using AspNetCore.Swagger.Themes;

using CICDTemplate.Application;
using CICDTemplate.Infrastructure;
using CICDTemplate.ServiceDefaults;

using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddDaprClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder
    .Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

using var meter = new Meter("CICDTemplate.Counter");
var counter = meter.CreateCounter<int>("cron_counter");
builder.Services.AddSingleton(meter);
builder.Services.AddSingleton(counter);

WebApplication app = builder.Build();

app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(
        ModernStyle.Futuristic,
        options => options.SwaggerEndpoint("/openapi/v1.json", "CICCDTemplate API V1"));

    app.UseReDoc(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
        options.DocumentTitle = "CICDTemplate API Documentation";
    });

    app.MapScalarApiReference();
}

app.UseAuthorization();
app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();
await app.RunAsync();

public partial class Program
{
    protected Program()
    {
    }
}