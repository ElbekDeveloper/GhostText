using GhostText.Data;
using GhostText.Models;
using GhostText.Repositories;
using GhostText.Services;
using GhostText.TelegramClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();  
builder.Services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
builder.Services.AddScoped<ITelegramUserService, TelegramUserService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Telegram Bot Configuration
builder.Services.Configure<TelegramSettings>(
    builder.Configuration.GetSection("TelegramSettings"));
builder.Services.AddSingleton<TelegramClient>();

var app = builder.Build();

// telegramClient is used to listen for incoming messages from the Telegram Bot
var telegramClient = app.Services.GetRequiredService<TelegramClient>();
telegramClient.ListenTelegramBot();

if (app.Environment.IsDevelopment())
{    
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();