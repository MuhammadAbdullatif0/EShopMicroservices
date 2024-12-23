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


        var app = builder.Build();

        app.MapCarter();

        app.Run();
    }
}
