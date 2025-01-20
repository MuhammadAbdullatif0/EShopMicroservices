using BuildingBlocks.Exceptions.Handler;
using Catalog.Api.Data;

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
            configuration.AddOpenBehavior(typeof(LoggingBehaviours<,>));
        });

        builder.Services.AddValidatorsFromAssembly(assemply);

        builder.Services.AddCarter();

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("catalogdb")!);
        }).UseLightweightSessions();

        if(builder.Environment.IsDevelopment())
        {
            builder.Services.InitializeMartenWith<CatalogInitialData>();
        }

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        var app = builder.Build();

        app.MapCarter();

        app.UseEndpoints(endpoints => { });

        app.Run();
    }
}
