using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportManagement.Infrastructure.SQLRepo;

public class SQLSupportTicketRepo(SQLDbContext _context): ISupportTicketRepo
{
    public async Task<List<SupportTicket>> GetAll()
    {
        return await _context.SupportTickets.ToListAsync();
    }

    public async Task<SupportTicket?> GetById(Guid id)
    {
        return await _context.SupportTickets.FindAsync(id);
    }

    public async Task Create(SupportTicket supportTicket)
    {
        await _context.SupportTickets.AddAsync(supportTicket);
        await _context.SaveChangesAsync();
    }

    public async Task<SupportTicket?> Update(SupportTicket supportTicket)
    {
        var existingSupportTicket = await _context.SupportTickets.FindAsync(supportTicket.Id);

        if (existingSupportTicket == null)
            return null;

        foreach (var property in typeof(SupportTicket).GetProperties())
        {
            var newValue = property.GetValue(supportTicket);
            var currentValue = property.GetValue(existingSupportTicket);

            if (newValue != null && newValue != currentValue)
            {
                property.SetValue(existingSupportTicket, newValue);
            }
        }

        _context.SupportTickets.Update(existingSupportTicket);
        await _context.SaveChangesAsync();

        return existingSupportTicket;
    }

    public async Task<SupportTicket?> Delete(Guid id)
    {
        var supportTicket = await _context.SupportTickets.FindAsync(id);

        if (supportTicket == null) 
            return null;

        _context.SupportTickets.Remove(supportTicket);
        await _context.SaveChangesAsync();

        return supportTicket;
    }

    public async Task UpdateAllByUserId(Guid userId, SupportTicket supportTicket)
    {
        var existingSupportTickets = await _context.SupportTickets.Where(s => s.customerId == userId).ToListAsync();

        foreach (var existingSupportTicket in existingSupportTickets)
        {
            foreach (var property in typeof(SupportTicket).GetProperties())
            {
                var newValue = property.GetValue(supportTicket);
                var currentValue = property.GetValue(existingSupportTicket);

                if (newValue != null && newValue != currentValue)
                {
                    property.SetValue(existingSupportTicket, newValue);
                }
            }

            _context.SupportTickets.Update(existingSupportTicket);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllByUserId(Guid userId)
    {
        var supportTickets = await _context.SupportTickets.Where(s => s.customerId == userId).ToListAsync();

        foreach (var supportTicket in supportTickets)
        {
            _context.SupportTickets.Remove(supportTicket);
        }

        await _context.SaveChangesAsync();
    }
}