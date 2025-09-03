using CPI_Backend.API.Models.Purchasesc;
using CPI_Backend.API.Models.Users;
using CPI_Backend.API.Models.Products;
using CPI_Backend.API.Models.Clients;

using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
     public DbSet<Client> Clients { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<LogicalCost> LogicalCosts { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Transaction> Transactions { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Product>()
            .ToTable("Products")
            .Property(p => p.Value)
            .HasPrecision(18, 2);// modelBuilder.Entity<Client>().ToTable("Client"); // o "Clientes" si así se llama tu tabla
                                 // modelBuilder
                                 //     .Entity<Purchase>()
                                 //     .HasOne(c => c.Clients)
                                 //     .WithMany() // si no tienes navegación inversa
                                 //     .HasForeignKey(c => c.Doc_Identidad);
        //Configuraciones de relaciones
         modelBuilder.Entity<Client>()
            .HasOne(c => c.Role)
            .WithMany(r => r.Clients)
            .HasForeignKey(c => c.RoleCode)
            .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Purchases)
            .HasForeignKey(p => p.IdentityDoc)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Product)
            .WithMany(pr => pr.Purchases)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LogicalCost>()
            .HasOne(lc => lc.Purchase)
            .WithOne(p => p.LogicalCost)
            .HasForeignKey<LogicalCost>(lc => lc.OrderNumber)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Client)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.IdentityDoc)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Purchase)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.OrderNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Purchase)
            .WithMany(p => p.Transactions)
            .HasForeignKey(t => t.OrderNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Invoice)
            .WithMany(i => i.Transactions)
            .HasForeignKey(t => t.InvoiceNumber) 
            .OnDelete(DeleteBehavior.Restrict);

            
          // Índices para mejor performance
        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Category);

        modelBuilder.Entity<Purchase>()
            .HasIndex(p => p.PurchaseDate);

        modelBuilder.Entity<Transaction>()
            .HasIndex(t => t.TransactionStatus);

        // Seeds de datos iniciales (roles por defecto)
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleCode = "ADMIN", RoleName = "Admin" },
            new Role { RoleCode = "CLIENT", RoleName = "Client" },
            new Role { RoleCode = "USER", RoleName = "User" }
        );
         modelBuilder.Entity<Product>()
            .Property(p => p.Value)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.ProductValue)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.ExchangeRate)
            .HasPrecision(18, 4);

        // Configurar todas las propiedades decimales de LogicalCost
        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.InternationalTransport)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.LocalTransport)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.Nationalization)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.CargoInsurance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.Storage)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LogicalCost>()
            .Property(lc => lc.Others)
            .HasPrecision(18, 2);

        // Configurar decimales de Invoice
        modelBuilder.Entity<Invoice>()
            .Property(i => i.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.Vat)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.ExchangeRate)
            .HasPrecision(18, 4);

        modelBuilder.Entity<Invoice>()
            .Property(i => i.Total)
            .HasPrecision(18, 2);
    }
}
