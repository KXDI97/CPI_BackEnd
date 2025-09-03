using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CPI_Backend.API.Data;
using CPI_Backend.API.DTO.Purchase;         // Para TransactionDto
using CPI_Backend.API.Models.Purchasesc;       // Para Transaction, Purchase, Invoice
using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Endpoints;

public static class TransactionsEndpoint
{
    public static void MapTransactionsEndpoints(this IEndpointRouteBuilder app)
    {
        // GET - Obtener todas las transacciones
        app.MapGet("/transactions", async (AppDbContext db) =>
        {
            var transactions = await db.Transactions
                .Include(t => t.Purchase)
                .ThenInclude(p => p.Client)
                .Include(t => t.Invoice)
                .Select(t => new TransactionDto
                {
                    TransactionNumber = t.TransactionNumber,
                    OrderNumber = t.OrderNumber,
                    InvoiceNumber = t.InvoiceNumber, // Corregir typo en el modelo
                    Reminder = t.Reminder,
                    TransactionStatus = t.TransactionStatus,
                    PaymentDate = t.PaymentDate,
                    ClientName = t.Purchase.Client.Username,
                    Amount = t.Invoice.Total
                })
                .ToListAsync();

            return Results.Ok(transactions);
        });

        // POST - Crear nueva transacción
        app.MapPost("/transactions", async (CreateTransactionDto input, AppDbContext db) =>
        {
            // Validar que exista la compra
            var purchase = await db.Purchases.FirstOrDefaultAsync(p =>
                p.OrderNumber == input.OrderNumber);
            if (purchase == null)
            {
                return Results.BadRequest("Purchase order not found");
            }

            // Validar que exista la factura
            var invoice = await db.Invoices.FirstOrDefaultAsync(i =>
                i.InvoiceNumber == input.InvoiceNumber);
            if (invoice == null)
            {
                return Results.BadRequest("Invoice not found");
            }

            var transaction = new Transaction
            {
                OrderNumber = input.OrderNumber,
                InvoiceNumber = input.InvoiceNumber,
                Reminder = input.Reminder,
                TransactionStatus = input.TransactionStatus,
                PaymentDate = input.PaymentDate
            };

            db.Transactions.Add(transaction);
            await db.SaveChangesAsync();

            return Results.Created($"/transactions/{transaction.TransactionNumber}", new TransactionDto
            {
                TransactionNumber = transaction.TransactionNumber,
                OrderNumber = transaction.OrderNumber,
                InvoiceNumber = transaction.InvoiceNumber,
                Reminder = transaction.Reminder,
                TransactionStatus = transaction.TransactionStatus,
                PaymentDate = transaction.PaymentDate,
                ClientName = "", // O buscar el nombre del cliente si lo necesitas
                Amount = 0 // O buscar el monto de la factura si lo necesitas
            });
        });

        // PUT - Actualizar estado de transacción
        app.MapPut("/transactions/{transactionNumber:int}/status",
            async (int transactionNumber, UpdateTransactionStatusDto input, AppDbContext db) =>
        {
            var transaction = await db.Transactions.FindAsync(transactionNumber);
            if (transaction is null)
                return Results.NotFound();

            transaction.TransactionStatus = input.TransactionStatus;
            if (input.PaymentDate.HasValue)
            {
                transaction.PaymentDate = input.PaymentDate.Value;
            }

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // GET - Transacciones por estado
        app.MapGet("/transactions/status/{status}", async (string status, AppDbContext db) =>
        {
            var transactions = await db.Transactions
                .Include(t => t.Purchase)
                .ThenInclude(p => p.Client)
                .Include(t => t.Invoice)
                .Where(t => t.TransactionStatus.ToLower() == status.ToLower())
                .Select(t => new TransactionSummaryDto
                {
                    TransactionNumber = t.TransactionNumber,
                    TransactionStatus = t.TransactionStatus,
                    PaymentDate = t.PaymentDate,
                    Amount = t.Invoice.Total,
                    ClientName = t.Purchase.Client.Username
                })
                .ToListAsync();

            return Results.Ok(transactions);
        });
    }
}