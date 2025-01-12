namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", request);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with Id {ProductId} not found", request.Id);
            throw new ProductNotFoundException(request.Id);
        }

        return new GetProductByIdResult(product);
    }
}

