public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : base("Product" , productId)
    {
    }
    public ProductNotFoundException(string Category) : base($"Product with Category {Category} not found")
    {
    }
}
