using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIRevendeurs.Models;
using Microsoft.IdentityModel.Tokens;

namespace APIRevendeurs
{
    
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly List<User> Users = new List<User>()
        {
            new User
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@gmail.com",
                Password = "PwdAdmin"
            }
        };
        
        public User Authenticate(string email, string password)
        {
            return Users.Where(u => u.Email.ToUpper().Equals(email.ToUpper())
                                    && u.Password.Equals(password)).FirstOrDefault();
        }
        
        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        

    }
}