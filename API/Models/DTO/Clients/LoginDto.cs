using CPI_Backend.API.Data;
using CPI_Backend.API.DTO.Purchase;         // Para InvoiceDto
using CPI_Backend.API.Models.Clients;        // Para relaciones
using CPI_Backend.API.Models.Products;       // Para relaciones
using CPI_Backend.API.Models.Purchasesc;       // Para Invoice, Purchase
using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Endpoints;
public class LoginDto
{
    public required string IdentityDoc { get; set; }
    public required string Password { get; set; }
}