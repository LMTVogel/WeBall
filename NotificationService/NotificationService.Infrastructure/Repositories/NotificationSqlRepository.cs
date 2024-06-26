using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Repositories;

public class NotificationSqlRepository(SqlDbContext ctx) : IRepository<Notification>
{
    public Notification GetById(Guid id)
    {
        return ctx.Notifications.First(x => x.Id == id);
    }

    public IQueryable<Notification> GetAll()
    {
        return ctx.Notifications;
    }

    public void Add(Notification entity)
    {
        ctx.Add(entity);
        ctx.SaveChanges();
    }

    public void Update(Notification entity)
    {
        ctx.Update(entity);
        ctx.SaveChanges();
    }

    public void Delete(Notification entity)
    {
        ctx.Remove(entity);
        ctx.SaveChanges();
    }
}