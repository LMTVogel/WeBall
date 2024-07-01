using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace InventoryManagement.Infrastructure.SqlRepo;

public class SqlDbContext(DbContextOptions<SqlDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>();
    }

    public void Migrate()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}