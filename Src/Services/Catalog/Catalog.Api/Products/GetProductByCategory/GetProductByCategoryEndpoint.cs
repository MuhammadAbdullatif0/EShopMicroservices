
namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (ISender sender, string category) =>
        {
            var result = await sender.Send(new GetProductByCategoryRequest(category));
            if (result == null)
            {
                return Results.NotFound();
            }
            var res = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(res);
        })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get product by category")
            .WithDescription("Get product in the catalog by category");
    }
}
