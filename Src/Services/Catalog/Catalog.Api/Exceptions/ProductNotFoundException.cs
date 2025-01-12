public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId) : base($"Product with Id {productId} not found")
    {
    }
    public ProductNotFoundException(string Category) : base($"Product with Category {Category} not found")
    {
    }
}
