using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBIILadder.Shared.Services;

namespace MBIILadder.WebApp.Controllers
{
    public class PlayersController : ControllerBase
    {
        IFirebase _firebase;
        public PlayersController(IFirebase firebase)
        {
            _firebase = firebase;
        }

        public async Task<IActionResult> List()
        {
            return Ok((await _firebase.GetPlayersAsync()).Values);
        }
    }
}
