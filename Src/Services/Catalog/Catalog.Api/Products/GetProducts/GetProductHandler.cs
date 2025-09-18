using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

public record GetProductQuery(int? PageNumber = 1 , int? PageSize = 10):IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);
internal class GetProductHandler(IDocumentSession session , ILogger<GetProductHandler> logger) : 
    IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductQueryHandler.Handel called with {@Query}" , query);
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1 , query.PageSize ?? 10);
        return new GetProductResult(products);
    }
}
