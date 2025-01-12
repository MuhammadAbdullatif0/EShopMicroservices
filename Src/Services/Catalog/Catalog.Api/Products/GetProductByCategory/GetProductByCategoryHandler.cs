
namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryHandler(IDocumentSession session , ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryRequest, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying products by category");
        var products =  await session.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync(cancellationToken);
        if(products == null)
        {
            logger.LogWarning("No products found for category {CategoryId}", request.Category);
            throw new ProductNotFoundException(request.Category);
        }
        return new GetProductByCategoryResult(products);
    }
}
