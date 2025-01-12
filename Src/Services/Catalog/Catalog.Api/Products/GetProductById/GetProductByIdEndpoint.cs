namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            if (result == null)
            {
                return Results.NotFound();
            }
            var res = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(res);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get product by Id")
        .WithDescription("Get product in the catalog by Id");
    }
}
