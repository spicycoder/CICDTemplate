using AspNetCore.Swagger.Themes;

using CICDTemplate.Api.Extensions;
using CICDTemplate.Application;
using CICDTemplate.Infrastructure;
using CICDTemplate.ServiceDefaults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddDaprClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ModernStyle.Futuristic);
    await app.MigrateAndSeed();
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