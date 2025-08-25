namespace GhostText.Services.Requests;

public interface IRequestService
{
    bool ContainsForbiddenWord(string messageText);
}