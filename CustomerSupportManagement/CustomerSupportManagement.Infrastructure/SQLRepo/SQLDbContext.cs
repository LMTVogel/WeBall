using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportManagement.Infrastructure.SQLRepo;

public class SQLDbContext : DbContext
{
    public DbSet<SupportAgent> SupportAgents { get; set; }
    
    public SQLDbContext(DbContextOptions<SQLDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportAgent>().HasIndex(s => s.Email).IsUnique();
    }
}