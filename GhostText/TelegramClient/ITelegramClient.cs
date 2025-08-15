namespace GhostText.TelegramClient
{
    public interface ITelegramClient 
    {
        void ListenTelegramBot();
        void StopListening();
    }
}
