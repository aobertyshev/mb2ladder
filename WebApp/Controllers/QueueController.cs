using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Shared.Contexts;
using Shared.Models;

namespace MBIILadder.WebApp.Controllers
{
    [Authorize]
    public class QueueController : ControllerBase
    {
        private readonly LadderDbContext _db;
        public QueueController(LadderDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Create()
        {
            await _db.Queues.AddAsync(new Queue
            {
                Id = Guid.Empty
            });
            return Ok();
        }

        public async Task<IActionResult> Join()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var id = user?.FindFirst("Id").Value;
            var queue = await _db.Queues.SingleOrDefaultAsync(dbQueue => dbQueue.Id == Guid.Empty);
            queue.UserIds ??= new Guid[0];
            _db.Queues.Update(queue);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
