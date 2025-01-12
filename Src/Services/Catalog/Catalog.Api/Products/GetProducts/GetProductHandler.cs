namespace Catalog.Api.Products.GetProducts;

public record GetProductQuery():IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);
internal class GetProductHandler(IDocumentSession session , ILogger<GetProductHandler> logger) : 
    IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductQueryHandler.Handel called with {@Query}" , query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductResult(products);
    }
}
