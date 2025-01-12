namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool isDeleted);

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is requierd");
    }
}

internal class DeleteProductHandler(IDocumentSession session , ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
       logger.LogInformation("Deleting product with id {Id}", request.Id);
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with id {Id} not found", request.Id);
            throw new ProductNotFoundException(request.Id);
        }
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
