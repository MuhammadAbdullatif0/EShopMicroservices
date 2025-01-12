
namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
);
public record UpdateProductResponse(bool isUpdated);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products" , async (ISender sender, UpdateProductRequest req) =>
        {
            var command = req.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var res = new UpdateProductResponse(result.isUpdated);
            //var res = result.Adapt<UpdateProductResponse>();
            return Results.Ok(res);
        })
       .WithName("UpdateProduct")
       .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Update a product")
       .WithDescription("Update a product in the catalog");
    }
}
