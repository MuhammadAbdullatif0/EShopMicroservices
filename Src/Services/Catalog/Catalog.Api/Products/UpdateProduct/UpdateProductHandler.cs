namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
):ICommand<UpdateProductResult>;

public record UpdateProductResult(bool isUpdated);

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is requierd");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requierd")
            .Length(2,150).WithMessage("Name must be between 2 and 150 Char");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must be > 0");
    }
}

public class UpdateProductHandler(IDocumentSession session , ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with name {@Request}", request);
        var product = await session.LoadAsync<Product>(request.Id,cancellationToken);
        if(product == null)
        {
            logger.LogWarning("Product with id {Id} not found", request.Id);
            throw new ProductNotFoundException(request.Id);
        }
        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
