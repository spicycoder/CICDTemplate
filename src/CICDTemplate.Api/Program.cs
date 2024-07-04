using CICDTemplate.Api.Extensions;
using CICDTemplate.Application;
using CICDTemplate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddDefaultHealthChecks();
builder.AddDefaultOpenTelemetry();

builder
    .Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.MigrateAndSeed();
}

app.MapHealthChecksEndpoints();
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