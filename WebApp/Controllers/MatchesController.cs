// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Shared.Contexts;
// using Shared.Models;
//
// namespace WebApp.Controllers
// {
//     public class MatchesController : ControllerBase
//     {
//         private readonly LadderDbContext _db;
//         public MatchesController(
//             LadderDbContext db
//             )
//         {
//             _db = db;
//         }
//
//         [HttpGet]
//         public IActionResult List()
//         {
//             return Ok(_db.Matches);
//         }
//         
//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] Match match)
//         {
//             var now = DateTime.UtcNow;
//             await _db.Matches.AddAsync(new Match
//             {
//                 Id = Guid.NewGuid(),
//                 Date = now,
//                 DateUpdated = now,
//                 DateCreated = now,
//                 Maps = match.Maps,
//                 Score = string.Empty
//             });
//             await _db.SaveChangesAsync();
//             return Ok();
//         }
//     }
// }
