
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            var res = result.Adapt<GetProductResponse>();
            return Results.Ok(res);
        })
        .WithName("GetProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get product")
        .WithDescription("Get product in the catalog");
    }
}
