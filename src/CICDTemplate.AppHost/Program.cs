var builder = DistributedApplication.CreateBuilder(args);

var postgresDB = builder.AddPostgres("PostgresDbServer")
    .WithPgAdmin()
    .AddDatabase("cicdtemplatedb");

builder.AddProject<Projects.CICDTemplate_Api>("cicdtemplate-api")
    .WithReference(postgresDB);

await builder.Build().RunAsync();
