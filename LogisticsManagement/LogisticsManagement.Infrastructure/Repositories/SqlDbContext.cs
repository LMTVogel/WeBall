using LogisticsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace LogisticsManagement.Infrastructure.Repositories;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<LogisticsCompany> LogisticsCompanies { get; set; }

    public void Migrate()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}
