namespace Catalog.Api.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
);
public record CreateProductResponse(Guid Id);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest req, ISender sender) =>
        {
            var command = req.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var res = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{res.Id}", res);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new product")
        .WithDescription("Create a new product in the catalog");
    }
}
