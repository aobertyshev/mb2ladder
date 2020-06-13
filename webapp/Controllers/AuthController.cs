using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MBIILadder.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using MBIILadder.WebApp.Models;
// using MBIILadder.WebApp.Contexts;
using System.Security.Cryptography;

namespace MBIILadder.WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        IToken _tokenManager;
        public AuthController(IToken tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // var savedPasswordHash = await HashPasswordAsync(model.Password);
            var user = new User
            {
                Nick = model.Nick,
                Email = model.Email,
                Password = model.Password
            };
            // using (UserContext db = new UserContext())
            // {
            //     db.Users.Add(user);
            //     db.SaveChanges();
            // }
            return Ok();
        }

        // private async Task<string> HashPasswordAsync(string password)
        // {
        //     byte[] salt;
        //     new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        //     var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        //     byte[] hash = pbkdf2.GetBytes(20);
        //     byte[] hashBytes = new byte[36];
        //     Array.Copy(salt, 0, hashBytes, 0, 16);
        //     Array.Copy(hash, 0, hashBytes, 16, 20);
        //     return Convert.ToBase64String(hashBytes);
        // }

        // private async Task<bool> ArePasswordsEqual(string storedPassword, string modelPassword)
        // {
        //     byte[] hashBytes = Convert.FromBase64String(storedPassword);
        //     byte[] salt = new byte[16];
        //     Array.Copy(hashBytes, 0, salt, 0, 16);
        //     var pbkdf2 = new Rfc2898DeriveBytes(modelPassword, salt, 100000);
        //     byte[] hash = pbkdf2.GetBytes(20);
        //     for (int i = 0; i < 20; i++)
        //         if (hashBytes[i + 16] != hash[i])
        //             return false;
        //     return true;
        // }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] Login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // using (UserContext db = new UserContext())
            // {
            //     var user = db.Users.FirstOrDefault(user => user.Nick == model.Nick);
            //     if (user == null || !(await ArePasswordsEqual(user.Password, model.Password)))
            //     {
            //         return Unauthorized();
            //     }

            //     HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", _tokenManager.GenerateToken(60, new List<System.Security.Claims.Claim>()),
            //     new CookieOptions
            //     {
            //         MaxAge = TimeSpan.FromMinutes(60)
            //     });
            //     return Ok();
            // }
            return Ok();
        }
    }
}
