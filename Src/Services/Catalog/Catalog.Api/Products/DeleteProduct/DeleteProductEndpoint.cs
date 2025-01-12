namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductResponse(bool isDeleted);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid id ,ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var res = new DeleteProductResponse(result.isDeleted);
            //var res = result.Adapt<DeleteProductResponse>();
            return Results.Ok(res);
        })
       .WithName("DeleteProduct")
       .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Delete a product")
       .WithDescription("Delete a product from the catalog");
    }
}
