using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;
using Service.Interfaces;

namespace WebApi.Services
{
    public class AuthService : IAuthService
    {

        private static readonly string _secret = Guid.NewGuid().ToString();
        public static byte[] Key => Encoding.ASCII.GetBytes(_secret);

        private IEntityService<User> _usersService;

        public AuthService(IEntityService<User> usersService)
        {
            _usersService = usersService;
        }

        public string Authenticate(User user)
        {
            if(user == null)
                return null;

                //BCrypt - hashowanie hasel
            user = _usersService.ReadAsync().Result.SingleOrDefault(x => x.Password == user.Password && x.Username == user.Username);
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptior = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Id == 1 ? "admin" : "user"),
                new Claim(ClaimTypes.Role, user.Id == 1 ? UserRoles.Delete.ToString() : ""),
                new Claim(ClaimTypes.Role, UserRoles.Create.ToString()),
                new Claim(ClaimTypes.Role, UserRoles.Read.ToString()),
                new Claim(ClaimTypes.Role, UserRoles.Update.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(token);

        }
    }
}