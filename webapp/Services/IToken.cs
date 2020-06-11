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
        private readonly string envSecretKey = Environment.GetEnvironmentVariable("MBII_LADDER_WEB_APP_TOKEN_SECRET_KEY");
        public Token()
        {
        }

        public string GenerateToken(double expirationTime, List<Claim> claims)
        {
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envSecretKey)), SecurityAlgorithms.HmacSha256),
                claims: claims
            ));
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