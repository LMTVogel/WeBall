using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using Polly;

namespace NotificationService.Infrastructure.Repositories;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<Notification> Notifications { get; set; }

    public void Migrate()
    {
        Policy.Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
}