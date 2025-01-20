using Marten.Schema;

namespace Catalog.Api.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if(await session.Query<Product>().AnyAsync())
        {
            return;
        }
        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
    {
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product 1",
            Description = "Description 1",
            Category = new List<string> { "Category 1" },
            ImageFile = "Image 1",
            Price = 100
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product 2",
            Description = "Description 2",
            Category = new List<string> { "Category 2" },
            ImageFile = "Image 2",
            Price = 200
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product 3",
            Description = "Description 3",
            Category = new List<string> { "Category 3" },
            ImageFile = "Image 3",
            Price = 300
        }
    }; 

}
