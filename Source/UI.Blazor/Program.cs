WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApp(builder.Configuration)
       .AddPersistence(builder.Configuration, builder.Environment)
       .AddIdentityAndAuth();

builder.Build()
       .ConfigureMiddleware()
       .Run();
