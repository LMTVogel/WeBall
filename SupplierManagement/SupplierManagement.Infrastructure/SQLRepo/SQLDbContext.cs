using Microsoft.EntityFrameworkCore;
using Polly;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SQLDbContext : DbContext
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public SQLDbContext(DbContextOptions<SQLDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>().HasIndex(s => s.RepEmail).IsUnique();

        modelBuilder.Entity<Product>().HasOne(s => s.Supplier);
    }

    public void Migrate()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}