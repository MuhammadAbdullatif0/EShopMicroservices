namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCarter();

        builder.Services.AddMediatR(configuration =>
        {
           configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("catalogdb")!);
        }).UseLightweightSessions();

        var app = builder.Build();

        app.MapCarter();

        app.Run();
    }
}
