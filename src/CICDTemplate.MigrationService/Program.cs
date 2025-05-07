using CICDTemplate.Infrastructure;
using CICDTemplate.MigrationService;
using CICDTemplate.ServiceDefaults;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder
    .AddNpgsqlDbContext<ApplicationDbContext>("cicdtemplatedb");

var host = builder.Build();

await host.RunAsync();