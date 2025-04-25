using Microsoft.EntityFrameworkCore;
using ProductsCrud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ProductDb>(opt => opt.UseInMemoryDatabase("ProductsDB"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI();  
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/api/products", async (ProductDb db) =>
        await db.Products.ToListAsync())
    .WithName("GetAllProducts")
    .WithOpenApi();

app.MapGet("/api/products/{id}", async (int id, ProductDb db)
        => await db.Products.FindAsync(id)
            is Product product
            ? Results.Ok(product)
            : Results.NotFound())
    .WithName("GetProductsById")
    .WithOpenApi();


app.MapPost("/api/products", async (Product product, ProductDb db) =>
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
    })
    .WithName("CreateProduct")
    .WithOpenApi();

app.MapPut("/api/products/{id}", async (int id, Product productInput, ProductDb db) =>
    {
        var product = await db.Products.FindAsync(id);
        if (product == null)
            return Results.NotFound();
        if (id != productInput.Id)
            return Results.BadRequest();
        product.Name = productInput.Name;
        product.Price = productInput.Price;
        await db.SaveChangesAsync();
        return Results.NoContent();
    })
    .WithName("UpdateProduct")
    .WithOpenApi();

app.MapDelete("/api/products/{id}", async (int id, ProductDb db) =>
    {
        var product = await db.Products.FindAsync(id);
        if (product == null)
            return Results.NotFound();
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.NoContent();
    })
    .WithName("DeleteProduct")
    .WithOpenApi();

app.Run();

