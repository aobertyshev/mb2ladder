using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MBIILadder.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using MBIILadder.Shared.Models;
using MBIILadder.WebApp.Models;
using System.Security.Cryptography;
using MBIILadder.Shared.Services;

namespace MBIILadder.WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        IToken _tokenManager;
        IFirebase _firebase;
        public AuthController(IToken tokenManager, IFirebase firebase)
        {
            _tokenManager = tokenManager;
            _firebase = firebase;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if ((await _firebase.GetUsersAsync()).Values.SingleOrDefault(user => user.Email == model.Email) != null)
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
                Password = await HashPasswordAsync(model.Password),
                ConfirmedEmail = false,
                RegisterDate = DateTime.UtcNow
            };
            var player = new Player
            {
                Id = playerId,
                UserId = userId,
                Nick = model.Nick,
                ClanName = string.Empty,
                Region = string.Empty
            };
            await _firebase.CreateUserAsync(user);
            await _firebase.CreatePlayerAsync(player);
            return Ok(player);
        }

        private async Task<string> HashPasswordAsync(string password)
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

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] Login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = (await _firebase.GetUsersAsync()).Values.SingleOrDefault(user => user.Email == model.Email);
            if (user == null || !ArePasswordsEqual(user.Password, model.Password))
            {
                return Unauthorized();
            }

            var defaultExpirationInMinutes = 60;
            var player = await _firebase.GetPlayerAsync(user.PlayerId);
            HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", _tokenManager.GenerateToken(defaultExpirationInMinutes, new List<System.Security.Claims.Claim>()), new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(defaultExpirationInMinutes),
                SameSite = SameSiteMode.Strict,
                Secure = true,
                HttpOnly = true
            });
            HttpContext.Response.Cookies.Append(".AspNetCore.Application.Expires", DateTime.UtcNow.AddMinutes(defaultExpirationInMinutes).ToString(), new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(defaultExpirationInMinutes),
                SameSite = SameSiteMode.None,
                Secure = false,
                HttpOnly = false
            });
            return Ok(player);
        }
    }
}
