using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.API.Config;
using User.API.Entities;

namespace User.API.Services
{
    public class UserService: IUserService
    {
        #region Constructor
        private readonly SecuritySettings _securitySettings;
        private readonly List<UserDetails> _users;
        public UserService(IOptions<SecuritySettings> securitySettings)
        {
            _securitySettings = securitySettings.Value;

            _users = new List<UserDetails>();
            UsersSeed();
        }

        private void UsersSeed()
        {
            for(int i = 1; i <= 20; i++)
            {
                _users.Add(new UserDetails()
                {
                    Id = i,
                    FirstName = $"Tester{i:000}",
                    LastName = "Smith",
                    Username = $"Tester{i:000}Smith@test.com",
                    Password = "Abc123"
                });
            }
        }
        #endregion

        public async Task<IUserDetails> AuthenticateAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return null;
            }

            user.Token = GenerateJWTToken(user.Id);

            return user;
        }
        private string GenerateJWTToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securitySettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<IUserDetails>> GetAllAsync()
        {
            return _users;
        }
    }
}
