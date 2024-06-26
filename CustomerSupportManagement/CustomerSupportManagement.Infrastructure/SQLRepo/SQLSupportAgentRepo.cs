using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportManagement.Infrastructure.SQLRepo;

public class SQLSupportAgentRepo(SQLDbContext _context) : ISupportAgentRepo
{
    public async Task<List<SupportAgent>> GetAll()
    {
        return await _context.SupportAgents.ToListAsync();
    }

    public async Task<SupportAgent?> GetById(Guid id)
    {
        return await _context.SupportAgents.FindAsync(id);
    }

    public async Task Create(SupportAgent supportAgent)
    {
        await _context.SupportAgents.AddAsync(supportAgent);
        await _context.SaveChangesAsync();
    }

    public async Task<SupportAgent?> Update(SupportAgent supportAgent)
    {
        var existingSupportAgent = await _context.SupportAgents.FindAsync(supportAgent.Id);

        if (existingSupportAgent == null)
            return null;

        foreach (var property in typeof(SupportAgent).GetProperties())
        {
            var newValue = property.GetValue(supportAgent);
            var currentValue = property.GetValue(existingSupportAgent);

            if (newValue != null && newValue != currentValue)
            {
                property.SetValue(existingSupportAgent, newValue);
            }
        }

        _context.SupportAgents.Update(existingSupportAgent);
        await _context.SaveChangesAsync();

        return existingSupportAgent;
    }

    public async Task<SupportAgent?> Delete(Guid id)
    {
        var supportAgent = await _context.SupportAgents.FindAsync(id);

        if (supportAgent == null) 
            return null;

        _context.SupportAgents.Remove(supportAgent);
        await _context.SaveChangesAsync();

        return supportAgent;
    }
}