using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.Domain.Exceptions;
using CustomerSupportManagement.DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CustomerSupportManagement.DomainServices.Services;

public class SupportTicketService(ISupportTicketRepo supportTicketRepo, ISupportAgentService supportAgentService) : ISupportTicketService
{
    public async Task<IEnumerable<SupportTicket>> GetAll()
    {
        List<SupportTicket> supportTickets = await supportTicketRepo.GetAll();
        
        if (supportTickets.Count == 0)
            throw new HttpException("No support tickets found", 404);
        
        return supportTickets;
    }

    public async Task<SupportTicket> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support ticket id", 400);
        
        SupportTicket? supportTicket = await supportTicketRepo.GetById(guid);
        if (supportTicket == null)
            throw new HttpException("Support ticket not found", 404);
        
        return supportTicket;
    }

    public async Task Create(SupportTicket supportTicket)
    {
        supportTicket.status = true;
        
        //Throws error if support agent doesn't exist
        if(supportTicket.supportAgentId != null)
            await supportAgentService.GetById(supportTicket.supportAgentId.ToString()!);
        
        await supportTicketRepo.Create(supportTicket);
    }

    public async Task Update(string id, SupportTicket supportTicket)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support ticket id", 400);
        
        supportTicket.Id = guid;
        
        //Throws error if support agent doesn't exist
        if(supportTicket.supportAgentId != null)
            await supportAgentService.GetById(supportTicket.supportAgentId.ToString()!);
        
        if (await supportTicketRepo.Update(supportTicket) == null)
            throw new HttpException("Support ticket not found", 404);
        
    }

    public async Task Delete(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid support ticket id", 400);
        
        if(await supportTicketRepo.Delete(guid) == null)
            throw new HttpException("Support ticket not found", 404);
    }
    
    public async Task AssignSupportAgent(string ticketId, string agentId)
    {
        if(!Guid.TryParse(ticketId, out Guid ticketGuid))
            throw new HttpException("Invalid support ticket id", 400);
        
        if(!Guid.TryParse(agentId, out Guid agentGuid))
            throw new HttpException("Invalid support agent id", 400);
        
        //Throws error if support agent doesn't exist
        await supportAgentService.GetById(agentId);
        
        SupportTicket supportTicket = new SupportTicket { Id = ticketGuid, supportAgentId = agentGuid };
        await supportTicketRepo.Update(supportTicket);
    }
    
    public async Task Close(string id)
    {
        await Update(id, new SupportTicket() { status = false });
    }
}