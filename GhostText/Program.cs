using Coravel;
using Coravel.Scheduling.Schedule;
using GhostText.Data;
using GhostText.Repositories;
using GhostText.Repositories.TelegramBotConfigurations;
using GhostText.Services;
using GhostText.Services.TelegramBotConfigurations;
using GhostText.Services.TelegramBotListeners;
using GhostText.TelegramClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<ITelegramUserRepository, TelegramUserRepository>();
builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
builder.Services.AddTransient<ITelegramClient, TelegramClient>();
builder.Services.AddTransient<ITelegramBotConfigurationRepository, TelegramBotConfigurationRepository>();
builder.Services.AddTransient<ITelegramBotConfigurationService, TelegramBotConfigurationService>();
builder.Services.AddTransient<TelegramBotListenersService>();
builder.Services.AddScheduler();

WebApplication app = builder.Build();

app.Services.GetRequiredService<ITelegramClient>()
    .ListenTelegramBot();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<TelegramBotListenersService>()
        .EverySeconds(10).PreventOverlapping(nameof(TelegramBotListenersService));
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();