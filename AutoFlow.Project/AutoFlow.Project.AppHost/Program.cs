var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AutoFlow_Api>("autoflow-api");

builder.Build().Run();
