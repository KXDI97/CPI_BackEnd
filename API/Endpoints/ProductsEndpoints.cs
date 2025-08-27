using Microsoft.EntityFrameworkCore;
using CPI_Backend.API.Models.Products;
using CPI_Backend.API.Data;
using CPI_Backend.API.Models.DTO;


namespace CPI_Backend.API.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            // Obtener todos los productos
            app.MapGet("/api/products", async (AppDbContext db) =>
                await db.Products.ToListAsync());

            // Obtener producto por ID
            app.MapGet("/api/products/{id}", async (string id, AppDbContext db) =>
                await db.Products.FindAsync(id) is Product product
                    ? Results.Ok(product)
                    : Results.NotFound());

            // Crear producto
            app.MapPost("/api/products", async (Product input, AppDbContext db) =>
            {
                db.Products.Add(input);
                await db.SaveChangesAsync();
                return Results.Created($"/api/products/{input.ProductId}", input);
            });

            // Actualizar producto
            app.MapPut("/api/products/{id}", async (string id, Product update, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                product.Name = update.Name;
                product.Value = update.Value;
                product.Category = update.Category;
                product.Description = update.Description;
                product.Stock = update.Stock;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Eliminar producto
            app.MapDelete("/api/products/{id}", async (string id, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Aumentar stock de un producto existente
            app.MapPut("/api/products/{id}/add-stock", async (string id, AddStockDTO input, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound("Producto no encontrado");

                if (input.Quantity <= 0)
                    return Results.BadRequest("La cantidad debe ser mayor a cero.");

                product.Stock += input.Quantity;
                await db.SaveChangesAsync();

                return Results.Ok(new { message = "Stock actualizado correctamente", product });
            });

        }
    }
}
