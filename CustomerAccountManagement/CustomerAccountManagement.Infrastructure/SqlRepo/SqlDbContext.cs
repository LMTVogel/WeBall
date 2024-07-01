using CustomerAccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace CustomerAccountManagement.Infrastructure.SqlRepo;

public class SqlDbContext(DbContextOptions<SqlDbContext> options)
    : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }

    public void Migrate()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}