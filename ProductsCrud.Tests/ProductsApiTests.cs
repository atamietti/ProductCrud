using Microsoft.EntityFrameworkCore;

namespace ProductsCrud.Tests;

public class ProductsServiceTests
{
    private readonly ProductDb _dbContext;

    public ProductsServiceTests()
    {
        var options = new DbContextOptionsBuilder<ProductDb>()
            .UseInMemoryDatabase(databaseName: "TestProductDb_" + Guid.NewGuid())
            .Options;

        _dbContext = new ProductDb(options);

        _dbContext.Products.Add(new Product { Id = 1, Name = "Test Product 1", Price = 9.99M });
        _dbContext.Products.Add(new Product { Id = 2, Name = "Test Product 2", Price = 19.99M });
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        // Act  
        var products = await _dbContext.Products.ToListAsync();

        // Assert  
        Assert.NotNull(products);
        Assert.Equal(2, products.Count);
    }

    [Fact]
    public async Task GetProductById_WithValidId_ReturnsProduct()
    {
        // Act  
        var product = await _dbContext.Products.FindAsync(1);

        // Assert  
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("Test Product 1", product.Name);
    }

    [Fact]
    public async Task CreateProduct_AddsProductToDb()
    {
        // Arrange  
        var newProduct = new Product { Name = "New Test Product", Price = 29.99M };

        // Act  
        _dbContext.Products.Add(newProduct);
        await _dbContext.SaveChangesAsync();

        // Assert  
        var product = await _dbContext.Products.FindAsync(newProduct.Id);
        Assert.NotNull(product);
        Assert.Equal(newProduct.Name, product.Name);
        Assert.Equal(newProduct.Price, product.Price);
    }

    [Fact]
    public async Task UpdateProduct_ModifiesExistingProduct()
    {
        // Arrange  
        var product = await _dbContext.Products.FindAsync(2);
        Assert.NotNull(product);

        // Act  
        product.Name = "Updated Test Product";
        product.Price = 39.99M;
        await _dbContext.SaveChangesAsync();

        // Assert  
        var updatedProduct = await _dbContext.Products.FindAsync(2);
        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Test Product", updatedProduct.Name);
        Assert.Equal(39.99M, updatedProduct.Price);
    }

    [Fact]
    public async Task DeleteProduct_RemovesProductFromDb()
    {
        // Arrange  
        var product = await _dbContext.Products.FindAsync(1);
        Assert.NotNull(product);

        // Act  
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        // Assert  
        var deletedProduct = await _dbContext.Products.FindAsync(1);
        Assert.Null(deletedProduct);
    }
}