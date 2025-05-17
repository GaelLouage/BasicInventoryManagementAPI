using Infra.Models;
using Infra.Services.Classes;
using Infra.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductServiceOptions>(
        builder.Configuration.GetSection("ProductService")
    );

builder.Services.AddScoped<IProductService>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ProductServiceOptions>>().Value;
    return new ProductService(options.FilePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// get all products
app.MapGet("/products", async Task<IResult> (IProductService productService) =>
{
    var products = await productService.GetAllAsync();
    if(products is null || products.Count == 0) 
        return Results.NotFound("Products not found!");

    return Results.Ok(products);
})
.WithName("products")
.WithOpenApi();


// get all with stock lower than 5
app.MapGet("/low-stock", async Task<IResult> (IProductService productService) =>
{
    var productsWithLowStock = await productService.GetLowStockAsync(x => x.Quantity < 5);
    if (productsWithLowStock is null || productsWithLowStock.Count == 0)
        return Results.NotFound("Products not found!");

    return Results.Ok(productsWithLowStock);
})
.WithName("low-stock")
.WithOpenApi();


// add product
app.MapPost("/add-product", async Task<IResult> (IProductService productService, [FromBody] ProductEntity product) =>
{
    var productsAdded = await productService.Add(product);
    if (productsAdded is false)
    {
        return Results.Ok("Failed to add product.");
    }
        

    return Results.Ok($"Product added succesfully.");
})
.WithName("add-product")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
