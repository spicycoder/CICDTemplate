using CICDTemplate.Api.Extensions;
using CICDTemplate.Application;
using CICDTemplate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.Seed().ConfigureAwait(false);
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync().ConfigureAwait(false);