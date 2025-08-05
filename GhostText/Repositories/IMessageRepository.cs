using GhostText.Models;

namespace GhostText.Repositories;

public interface IMessageRepository
{
    Task<Message> InsertMessageAsync(Message message);
    IQueryable<Message> SelectAllMessages();
    Task<Message> SelectMessageById(Guid Id);
}