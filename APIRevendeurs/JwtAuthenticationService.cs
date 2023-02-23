using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIRevendeurs.Models;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

namespace APIRevendeurs
{
    
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        
        public User Authenticate(string email, string password)
        {
            List<User> users = new List<User>();
            string cs = "server=localhost;port=8889;database=api-revendeur;uid=root;pwd=root";
            using var connection = new MySqlConnection(cs);
            connection.Open();
            
            string sql = "SELECT * FROM user";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                User user = new User();
                user.Id = reader.GetInt32("id");
                user.Username = reader.GetString("username");
                user.Email = reader.GetString("email");
                user.Password = reader.GetString("password");

                users.Add(user);
            }
            connection.Close();
            
            return users.Where(u => u.Email.ToUpper().Equals(email.ToUpper())
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