
namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryRequest(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryRequest, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryRequest request, CancellationToken cancellationToken)
    {
        var products =  await session.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync(cancellationToken);
        if(products == null)
        {
            throw new ProductNotFoundException(request.Category);
        }
        return new GetProductByCategoryResult(products);
    }
}
