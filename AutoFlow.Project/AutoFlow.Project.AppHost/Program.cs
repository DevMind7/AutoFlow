var builder = DistributedApplication.CreateBuilder(args);

// Services Infrastructures

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("postgresdb");

// Services métier

var api = builder.AddProject<Projects.AutoFlow_Api>("autoflow-api");

builder.AddProject<Projects.AutoFlow_WebApp>("webApp")
    .WithReference(api);

builder.Build().Run();
