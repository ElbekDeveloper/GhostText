using Coravel;
using GhostText.Data;
using GhostText.Repositories;
using GhostText.Repositories.TelegramBotConfigurations;
using GhostText.Repositories.Users;
using GhostText.Services;
using GhostText.Services.Accounts;
using GhostText.Services.TelegramBotConfigurations;
using GhostText.Services.TelegramBotListeners;
using GhostText.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AuthConfiguration:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthConfiguration:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AuthConfiguration:Key"]!))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<ITelegramUserRepository, TelegramUserRepository>();
builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITelegramBotConfigurationRepository, TelegramBotConfigurationRepository>();
builder.Services.AddTransient<ITelegramBotConfigurationService, TelegramBotConfigurationService>();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();