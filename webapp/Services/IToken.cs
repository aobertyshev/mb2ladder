using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MBIILadder.WebApp.Services
{
    public interface IToken
    {
        string GenerateToken(double expirationTime, List<Claim> claims);
        List<Claim> GetAllClaimsFromToken(string token);
        string GetValueFromToken(string type, string token);
        ClaimsPrincipal ValidateToken(string token);
    }

    public class Token : IToken
    {
        private readonly string envSecretKey = Environment.GetEnvironmentVariable("MBIILadder_WebAppTokenSecretKey");
        public Token()
        {
        }

        public string GenerateToken(double expirationTime, List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envSecretKey));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: signInCredentials,
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        public List<Claim> GetAllClaimsFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tokens = handler.ReadToken(token) as JwtSecurityToken;
                return tokens.Payload.Claims.ToList();
            }
            catch
            {
                return null;
            }
        }

        public string GetValueFromToken(string type, string token) =>
            ValidateToken(token)?.FindFirst("ClaimName")?.Value;

        public ClaimsPrincipal ValidateToken(string token)
        {
            SecurityToken validatedToken;
            var validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.IssuerSigningKey =
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(envSecretKey));

            ClaimsPrincipal principal =
                new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }
    }
}