using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DataAccess;
using WebApplication.Options;

namespace WebApplication.Services
{
    public class AuthService
    {
        private readonly EarthquakeContext context;
        private readonly string jwtSecret;

        public AuthService(EarthquakeContext context, IOptions<SecretOption> secretOptions)
        {
            this.context = context;
            this.jwtSecret = secretOptions.Value.JWTSecret;
        }

        public async Task<string> Authenticate(string login, string password)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(user => user.Password == password && user.Username == login);

            if (existingUser == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = DateTime.UtcNow.AddYears(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> Registrate(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Username == login);

            if (!(user is null)) return null;

            context.Users.Add(new Models.User { Username = login, Password = password });
            await context.SaveChangesAsync();

            return "User Successfully registered";
        }
    }
}
