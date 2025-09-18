using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var assemply = typeof(Program).Assembly;
        builder.Services.AddMediatR(configuration =>
        {
           configuration.RegisterServicesFromAssembly(assemply);
            configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        if(builder.Environment.IsDevelopment())
        {
            builder.Services.InitializeMartenWith<CatalogInitialData>();
        }

        builder.Services.AddValidatorsFromAssembly(assemply);

        builder.Services.AddCarter();

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("catalogdb")!);
        }).UseLightweightSessions();

        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("catalogdb")!);

        var app = builder.Build();

        app.MapCarter();

        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;
                if (exception == null)
                {
                    return;
                }
                
                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = exception.Message,
                    Instance = exception.StackTrace
                };

                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, exception.Message);

                context.Response.ContentType = "application/proplem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            });
        });

        app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

        app.Run();
    }
}
