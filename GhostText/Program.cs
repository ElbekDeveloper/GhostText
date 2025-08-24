using Coravel;
using GhostText.Data;
using GhostText.Repositories;
using GhostText.Repositories.TelegramBotConfigurations;
using GhostText.Services;
using GhostText.Services.Levenshteins;
using GhostText.Services.Requests;
using GhostText.Services.TelegramBotConfigurations;
using GhostText.Services.TelegramBotListeners;
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
builder.Services.AddTransient<ILevenshteinService, LevenshteinService>();
builder.Services.AddTransient<ITelegramUserRepository, TelegramUserRepository>();
builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
builder.Services.AddTransient<ITelegramBotConfigurationRepository, TelegramBotConfigurationRepository>();
builder.Services.AddTransient<ITelegramBotConfigurationService, TelegramBotConfigurationService>();
builder.Services.AddTransient<ILevenshteinService, LevenshteinService>();
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddSingleton<TelegramBotListenersService>();
builder.Services.AddScheduler();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<TelegramBotListenersService>()
        .EverySeconds(1).PreventOverlapping(nameof(TelegramBotListenersService));
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();