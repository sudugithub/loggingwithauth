using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SpanTechnologyTask;
using SpanTechnologyTask.Middlewares;
using SpanTechnologyTask.Utils;

Log.Information("Starting up - pre init");

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Information("App Starting Up");

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    SwaggerProvider.Configure(builder.Services, builder.Configuration);
    AuthProvider.Configure(builder.Services, builder.Configuration);
    CorsProvider.Configure(builder.Services);

    ComponentRegistry.Register(builder.Services, builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Repository>();
        dbContext.Database.Migrate();
    }

    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}