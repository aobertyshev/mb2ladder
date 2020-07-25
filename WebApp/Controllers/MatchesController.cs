using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Contexts;
using Shared.Models;
using Shared.Services;

namespace WebApp.Controllers
{
    public class MatchesController : ControllerBase
    {
        private readonly LadderDbContext _db;
        public MatchesController(LadderDbContext db)
        {
            _db = db;
        }

        public IActionResult GetMatchesList()
        {
            return Ok(_db.Matches);
        }
        public async Task<IActionResult> Create()
        {
            await _db.Matches.AddAsync(new Match
            {
                Id = Guid.Empty
            });
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
