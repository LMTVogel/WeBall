using CustomerAccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccountManagement.Infrastructure.SqlRepo;

public class SqlDbContext(DbContextOptions<SqlDbContext> options)
    : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address);
    }
}