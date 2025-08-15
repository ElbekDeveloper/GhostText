namespace GhostText.Clients.TelegramClient
{
    public interface ITelegramClient 
    {
        void ListenTelegramBot();
        void StopListening();
    }
}
