using CustomerAccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
}