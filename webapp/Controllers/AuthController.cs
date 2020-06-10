using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MBIILadder.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MBIILadder.WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        IToken _tokenManager;
        public AuthController(IToken tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task<IActionResult> Register(Register model)
        {
            HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", _tokenManager.GenerateToken(60, new List<System.Security.Claims.Claim>()),
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(60)
            });
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else return Ok();
        }

        public async Task<IActionResult> SignIn()
        {
            return Ok();
        }
    }
}
