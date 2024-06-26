using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.Domain.Exceptions;
using CustomerSupportManagement.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CustomerSupportManagement.DomainServices.Services;

public class SupportAgentService(ISupportAgentRepo supportAgentRepo) : ISupportAgentService
{
    public async Task<IEnumerable<SupportAgent>> GetAll()
    {
        List<SupportAgent> supportAgents = await supportAgentRepo.GetAll();
        
        if (supportAgents.Count == 0)
            throw new HttpException("No support agents found", 404);
        
        return supportAgents;
    }

    public async Task<SupportAgent> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support agent id", 400);
        
        SupportAgent? supportAgent = await supportAgentRepo.GetById(guid);
        if (supportAgent == null)
            throw new HttpException("Support agent not found", 404);
        
        return supportAgent;
    }

    public async Task Create(SupportAgent supportAgent)
    {
        try
        {
            await supportAgentRepo.Create(supportAgent);
        }
        catch (DbUpdateException ex) when ((ex.InnerException as MySqlException)?.Number == 1062)
        {
            throw new HttpException("Support agent email already exists", 400);
        }
    }

    public async Task Update(string id, SupportAgent supportAgent)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support agent id", 400);
        
        supportAgent.Id = guid;

        try
        {
            if (await supportAgentRepo.Update(supportAgent) == null)
                throw new HttpException("Support agent not found", 404);
        }
        catch (DbUpdateException ex) when ((ex.InnerException as MySqlException)?.Number == 1062)
        {
            throw new HttpException("Support agent email already exists", 400);
        }
    }

    public async Task Delete(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support agent id", 400);
        
        if(await supportAgentRepo.Delete(guid) == null)
            throw new HttpException("Support agent not found", 404);
    }
}