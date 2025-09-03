using CPI_Backend.API.Data;
using CPI_Backend.API.DTO.Purchase;         // Para InvoiceDto
using CPI_Backend.API.Models.Clients;        // Para relaciones
using CPI_Backend.API.Models.Products;       // Para relaciones
using CPI_Backend.API.Models.Purchasesc;       // Para Invoice, Purchase
using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Endpoints;

public static class InvoicesEndpoint
{
    public static void MapInvoicesEndpoints(this IEndpointRouteBuilder app)
    {
        // GET - Obtener todas las facturas
        app.MapGet("/invoices", async (AppDbContext db) =>
        {
            var invoices = await db.Invoices
                .Include(i => i.Client)
                .Include(i => i.Product)
                .Select(i => new InvoiceDto
                {
                    InvoiceNumber = i.InvoiceNumber,
                    IdentityDoc = i.IdentityDoc,
                    ClientName = i.Client.Username,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    OrderNumber = i.OrderNumber,
                    Subtotal = i.Subtotal,
                    Vat = i.Vat,
                    ExchangeRate = i.ExchangeRate,
                    Total = i.Total
                })
                .ToListAsync();

            return Results.Ok(invoices);
        });

        // GET - Obtener factura por número
        app.MapGet("/invoices/{invoiceNumber:int}", async (int invoiceNumber, AppDbContext db) =>
        {
            var invoice = await db.Invoices
                .Include(i => i.Client)
                .Include(i => i.Product)
                .Where(i => i.InvoiceNumber == invoiceNumber)
                .Select(i => new InvoiceDto
                {
                    InvoiceNumber = i.InvoiceNumber,
                    IdentityDoc = i.IdentityDoc,
                    ClientName = i.Client.Username,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    OrderNumber = i.OrderNumber,
                    Subtotal = i.Subtotal,
                    Vat = i.Vat,
                    ExchangeRate = i.ExchangeRate,
                    Total = i.Total
                })
                .FirstOrDefaultAsync();

            return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
        });

        // POST - Crear nueva factura
        app.MapPost("/invoices", async (CreateInvoiceDto input, AppDbContext db) =>
        {
            // Validar que exista el cliente
            var client = await db.Clients.FindAsync(input.IdentityDoc);
            if (client == null)
            {
                return Results.BadRequest("Client not found");
            }

            // Validar que exista el producto
            var product = await db.Products.FindAsync(input.ProductId);
            if (product == null)
            {
                return Results.BadRequest("Product not found");
            }

            // Validar que exista la compra
            var purchase = await db.Purchases.FirstOrDefaultAsync(p => 
                p.OrderNumber == input.OrderNumber);
            if (purchase == null)
            {
                return Results.BadRequest("Purchase order not found");
            }

            var invoice = new Invoice
            {
                IdentityDoc = input.IdentityDoc,
                ProductId = input.ProductId,
                OrderNumber = input.OrderNumber,
                Subtotal = input.Subtotal,
                Vat = input.Vat,
                ExchangeRate = input.ExchangeRate,
                Total = input.Total
            };

            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();

            var invoiceDto = new InvoiceDto
            {
                InvoiceNumber = invoice.InvoiceNumber,
                IdentityDoc = invoice.IdentityDoc,
                ClientName = client.Username,
                ProductId = invoice.ProductId,
                ProductName = product.Name,
                OrderNumber = invoice.OrderNumber,
                Subtotal = invoice.Subtotal,
                Vat = invoice.Vat,
                ExchangeRate = invoice.ExchangeRate,
                Total = invoice.Total
            };

            return Results.Created($"/invoices/{invoice.InvoiceNumber}", invoiceDto);
        });

        // GET - Facturas por cliente
        app.MapGet("/invoices/client/{identityDoc}", async (string identityDoc, AppDbContext db) =>
        {
            var invoices = await db.Invoices
                .Include(i => i.Product)
                .Where(i => i.IdentityDoc == identityDoc)
                .Select(i => new InvoiceSummaryDto
                {
                    InvoiceNumber = i.InvoiceNumber,
                    ClientName = i.Client.Username,
                    ProductName = i.Product.Name,
                    Total = i.Total,
                    CreatedDate = DateTime.Now // Agregar campo de fecha en el modelo si es necesario
                })
                .ToListAsync();

            return Results.Ok(invoices);
        });
    }
}