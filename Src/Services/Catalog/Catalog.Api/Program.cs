using BuildingBlocks.Behaviours;

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

        builder.Services.AddValidatorsFromAssembly(assemply);

        builder.Services.AddCarter();

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("catalogdb")!);
        }).UseLightweightSessions();

        var app = builder.Build();

        app.MapCarter();


        app.Run();
    }
}
