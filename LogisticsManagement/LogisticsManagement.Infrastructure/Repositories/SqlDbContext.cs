using LogisticsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsManagement.Infrastructure.Repositories;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<LogisticsCompany> LogisticsCompanies { get; set; }
}
