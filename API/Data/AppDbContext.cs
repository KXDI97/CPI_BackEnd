//using CPI_Backend.API.Models.Para_organizar;
using CPI_Backend.API.Models.Users;
using CPI_Backend.API.Models.Products;

using Microsoft.EntityFrameworkCore;

namespace CPI_Backend.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    // public DbSet<Client> Clients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    }
}
