using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBIILadder.WebApp.Contexts;

namespace MBIILadder.WebApp.Controllers
{
    public class PlayersController : ControllerBase
    {
        public PlayersController()
        {
        }

        public async Task<IActionResult> GetPlayerList()
        {
            using (UserContext db = new UserContext())
            {
                return Ok(db.Users.ToList());
            }
        }
    }
}
