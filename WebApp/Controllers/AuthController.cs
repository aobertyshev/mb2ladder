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
using Shared.Services;

namespace WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IToken _tokenManager;
        private readonly LadderDbContext _db;
        private readonly ICrypto _crypto;
        public AuthController(
            IToken tokenManager,
            LadderDbContext db,
            ICrypto crypto)
        {
            _tokenManager = tokenManager;
            _db = db;
            _crypto = crypto;
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

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Password = _crypto.HashPassword(model.Password),
                ConfirmedEmail = false,
                RegisterDate = DateTime.UtcNow,
                Discord = model.Discord,
                Nick = model.Nick,
                Region = model.Region,
                ClanName = model.ClanName
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok();
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
            if (user == null || !_crypto.ArePasswordsEqual(user.Password, model.Password))
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
                // new Claim("Id", user.PlayerId.ToString()),
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
