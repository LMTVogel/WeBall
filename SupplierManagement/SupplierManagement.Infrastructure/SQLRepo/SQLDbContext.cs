using Microsoft.EntityFrameworkCore;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SQLDbContext : DbContext
{
    public DbSet<Supplier> Suppliers { get; set; }
    
    public SQLDbContext(DbContextOptions<SQLDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>().HasIndex(s => s.RepEmail).IsUnique();
    }
}