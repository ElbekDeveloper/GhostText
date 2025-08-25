using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GhostText.CustomExceptions;
using GhostText.Models.UserCredentials;
using GhostText.Models.Users;
using GhostText.Models.UserTokens;
using GhostText.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GhostText.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public AccountService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async ValueTask<UserToken> LoginAsync(UserCredential userCredential)
        {
            User maybeUser =
                await this.userRepository.SelectAllUsers()
                    .FirstOrDefaultAsync(user => 
                    user.Username == userCredential.Username && 
                    user.Password == userCredential.Password);

            if (maybeUser is null)
                throw new NotFoundException("User is not found with given username and password!");

            return this.GenerateUserToken(maybeUser);
        }

        private UserToken GenerateUserToken(User user)
        {
            string secretKey = this.configuration["AuthConfiguration:Key"];
            string issuer = this.configuration["AuthConfiguration:Issuer"];
            string audience = this.configuration["AuthConfiguration:Audience"];

            SymmetricSecurityKey securityKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            SigningCredentials credentials = 
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ClaimTypes.Role, user.Role.ToString()),
                new Claim ("FullName", $"{user.FirstName} {user.LastName}")
            };

            DateTime expirationDate = DateTime.Now.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expirationDate,
                signingCredentials: credentials);

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserToken
            {
                Token = tokenAsString,
                ExpirationDate = expirationDate
            };
        }
    }
}
