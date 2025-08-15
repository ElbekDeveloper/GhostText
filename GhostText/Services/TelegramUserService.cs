using System;
using System.Collections.Generic;
using GhostText.Models;
using GhostText.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
            var telegramUser =
                await this.telegramUserRepository.SelectTelegramUserByIdAsync(userId);
            if (telegramUser is null)
            {
                throw new KeyNotFoundException($"Telegram User with Id: {userId} not found");
            }

            return telegramUser;
        }
        public async Task<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser)
        {
            return await this.telegramUserRepository.UpdateTelegramUserAsync(telegramUser);
        }

        public async Task<TelegramUser> RemoveTelegramUserAsync(Guid userId)
        {
            var telegramUser =
                await this.telegramUserRepository.SelectTelegramUserByIdAsync(userId);

            if (telegramUser is null)
            {
                throw new KeyNotFoundException($"Telegram User with Id: {userId} not found");
            }

            return await this.telegramUserRepository.DeleteTelegramUserAsync(telegramUser);
        }

        public async ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser)
        {
            var maybeTelegramUser =
                await this.RetrieveAllTelegramUser().FirstOrDefaultAsync(user => user.Id == telegramUser.Id);

            if (maybeTelegramUser is not null)
            {
                throw new KeyNotFoundException("bu telegram Id bilan allaqachon kirilgan !");
            }
            else
            {
                return await this.AddTelegramUserAsync(telegramUser); 
            }
        }        
    }
}
