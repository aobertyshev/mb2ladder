using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBIILadder.Shared.Services;

namespace MBIILadder.WebApp.Controllers
{
    public class MatchesController : ControllerBase
    {
        IFirebase _firebase;
        public MatchesController(IFirebase firebase)
        {
            _firebase = firebase;
        }

        public async Task<IActionResult> GetMatchesList()
        {
            return Ok();
        }
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
    }
}
