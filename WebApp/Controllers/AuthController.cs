using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MBIILadder.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Shared.Models;
using MBIILadder.WebApp.Models;
using System.Security.Cryptography;
using Shared.Contexts;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IToken _tokenManager;
        private readonly LadderDbContext _db;
        public AuthController(IToken tokenManager, LadderDbContext db)
        {
            _tokenManager = tokenManager;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _db.Users.AnyAsync(dbUser => dbUser.Email == model.Email))
            {
                return Conflict();
            }

            var userId = Guid.NewGuid();
            var playerId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                PlayerId = playerId,
                Email = model.Email,
                Password = HashPassword(model.Password),
                ConfirmedEmail = false,
                RegisterDate = DateTime.UtcNow
            };
            await _db.Users.AddAsync(user);
            return Ok();
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        private bool ArePasswordsEqual(string storedPassword, string modelPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(modelPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        [Authorize]
        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Delete("MBIILadder.SessionKey");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] Login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _db.Users.SingleOrDefaultAsync(dbUser => dbUser.Email == model.Email);
            if (user == null || !ArePasswordsEqual(user.Password, model.Password))
            {
                return Unauthorized();
            }

            const int defaultExpirationInMinutes = 60;
            var defaultCookieOptions = new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(defaultExpirationInMinutes),
                SameSite = SameSiteMode.None,
                Secure = false,
                HttpOnly = false
            };
            HttpContext.Response.Cookies.Append("MBIILadder.SessionKey", _tokenManager.GenerateToken(defaultExpirationInMinutes, new List<Claim>
            {
                new Claim("Id", user.PlayerId.ToString()),
            }),
            // new CookieOptions
            // {
            //     MaxAge = TimeSpan.FromMinutes(defaultExpirationInMinutes),
            //     SameSite = SameSiteMode.Strict,
            //     Secure = true,
            //     HttpOnly = true
            // }
            defaultCookieOptions
            );
            HttpContext.Response.Cookies.Append("MBIILadder.ExpiryDate", DateTime.UtcNow.AddMinutes(defaultExpirationInMinutes).ToString(), defaultCookieOptions);
            return Ok();
        }
    }
}
