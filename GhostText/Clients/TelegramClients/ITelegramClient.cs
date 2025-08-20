namespace GhostText.Clients.TelegramClients
{
    public interface ITelegramClient 
    {
        void ListenTelegramBot();
        void StopListening();
    }
}
