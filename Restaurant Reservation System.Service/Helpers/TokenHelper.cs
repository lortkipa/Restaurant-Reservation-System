using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.RoleUser;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant_Reservation_System.Service.Helpers
{
    public class TokenHelper
    {
        public static string TokenGeneration(int userId, string username, List<RoleDTO> roles, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(25),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
