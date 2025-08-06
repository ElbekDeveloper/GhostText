using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public MessageRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<Message> InsertMessageAsync(Message message)
    {
        await this.applicationDbContext.Messages.AddAsync(message);
        await this.applicationDbContext.SaveChangesAsync();
        return message;
    }

    public IQueryable<Message> SelectAllMessages()
    {
        IQueryable<Message> messages = 
            this.applicationDbContext.Messages;
        return messages;
    }

    public async Task<Message> SelectMessageById(Guid id)
    {
        var result = 
            await this.applicationDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<Message> UpdateMessageAsync(Message message)
    {
        this.applicationDbContext.Messages.Update(message);
        await this.applicationDbContext.SaveChangesAsync();
        return message;
    }

    public async Task<Message> DeleteMessageAsync(Message message)
    {
        this.applicationDbContext.Messages.Remove(message);
        await this.applicationDbContext.SaveChangesAsync();
        return message;
    }
}