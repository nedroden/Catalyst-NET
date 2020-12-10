using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;

using Catalyst.Api.Main.Models;

namespace Catalyst.Api.Main.Services
{
    public class TokenService
    {
        public IConfiguration _configuration;
        public TokenService(IConfiguration configuration) => _configuration = configuration;

        // This method is heavily inspired by the example provided here:
        // https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // The user's id, email and username are verified during requests.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name)
            };

            double numberOfDays;
            if (!Double.TryParse(_configuration["Authentication:JwtExpireDays"], out numberOfDays))
            {
                numberOfDays = 30;
            }

            var token = new JwtSecurityToken
                (_configuration["Authentication:JwtIssuer"],
                    _configuration["Authentication:JwtIssuer"],
                    claims,
                    expires: DateTime.Now.AddDays(numberOfDays),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}