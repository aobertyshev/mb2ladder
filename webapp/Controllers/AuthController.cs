using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MBIILadder.WebApp.Controllers
{
    public class AuthController : ControllerBase
    {
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else return Ok();
        }
    }
}
