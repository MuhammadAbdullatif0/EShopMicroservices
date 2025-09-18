
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);
public record GetProductRequest(int? PageNumber = 1 , int? PageSize = 10);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequest productRequest , ISender sender) =>
        {
            var query = productRequest.Adapt<GetProductQuery>();
            var result = await sender.Send(query);
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
