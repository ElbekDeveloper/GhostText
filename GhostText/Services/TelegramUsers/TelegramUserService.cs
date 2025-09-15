using System;
using System.Collections.Generic;
using GhostText.Models;
using GhostText.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserRepository telegramUserRepository;

        public TelegramUserService(ITelegramUserRepository telegramUserRepository) =>
            this.telegramUserRepository = telegramUserRepository;

        public async ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser) 
        {
            if (telegramUser is null)
                throw new ArgumentNullException("TelegramUser cannot be null.");

            return await this.telegramUserRepository.InsertTelegramUserAsync(telegramUser);
        } 

        public IQueryable<TelegramUser> RetrieveAllTelegramUser() =>
            this.telegramUserRepository.SelectAllTelegramUser();

        public async ValueTask<TelegramUser> RetrieveTelegramUserByIdAsync(Guid userId)
        {
            TelegramUser telegramUser =
                await this.telegramUserRepository.SelectTelegramUserByIdAsync(userId);

            if (telegramUser is null)
                throw new KeyNotFoundException($"Telegram User with Id: {userId} not found");

            return telegramUser;
        }

        public async ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser)
        {
            TelegramUser storageUser = await this.telegramUserRepository
                .SelectTelegramUserByIdAsync(telegramUser.Id);

            if (storageUser is null)
                throw new KeyNotFoundException($"TelegramUser with ID {telegramUser.Id} not found.");

            return await this.telegramUserRepository.UpdateTelegramUserAsync(telegramUser);
        }

        public async ValueTask<TelegramUser> RemoveTelegramUserAsync(Guid userId)
        {
            TelegramUser telegramUser =
                await this.telegramUserRepository.SelectTelegramUserByIdAsync(userId);

            if (telegramUser is null)
                throw new KeyNotFoundException($"Telegram User with Id: {userId} not found");

            return await this.telegramUserRepository.DeleteTelegramUserAsync(telegramUser);
        }

        public async ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser)
        {
            TelegramUser maybeTelegramUser =
                await this.RetrieveAllTelegramUser()
                    .FirstOrDefaultAsync(user => 
                        user.TelegramId == telegramUser.TelegramId);

            return maybeTelegramUser ?? await this.AddTelegramUserAsync(telegramUser);
        }
    }
}
