using System;
using System.Collections.Generic;
using GhostText.Models;
using GhostText.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserRepository telegramUserRepository;

        public TelegramUserService(ITelegramUserRepository telegramUserRepository)
        {
            this.telegramUserRepository = telegramUserRepository;
        }

        public async Task<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser)
        {
            return await this.telegramUserRepository.InsertTelegramUserAsync(telegramUser);
        }

        public IQueryable<TelegramUser> RetrieveAllTelegramUser()
        {
            return this.telegramUserRepository.SelectAllTelegramUser();
        }

        public async Task<TelegramUser> RetrieveTelegramUserByIdAsync(Guid userId)
        {
            var telegramUser=
                await this.telegramUserRepository.SelectTelegramUserById(userId);
            if (telegramUser is null)
            {
                throw new KeyNotFoundException($"Telegram User with Id: {userId} not found");
            }
            
            return telegramUser;
        }
    }
}
