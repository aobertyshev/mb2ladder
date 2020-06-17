using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MBIILadder.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MBIILadder.WebApp.Controllers
{
    [Authorize]
    public class QueueController : ControllerBase
    {
        IFirebase _firebase;
        public QueueController(IFirebase firebase)
        {
            _firebase = firebase;
        }

        public async Task<IActionResult> Create()
        {
            var queue = new MBIILadder.Shared.Models.Queue
            {
                Id = Guid.Empty
            };
            await _firebase.CreateQueueAsync(queue);
            return Ok();
        }

        public async Task<IActionResult> Join()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var id = user?.FindFirst("Id").Value;
            var queue = await _firebase.GetQueueAsync(Guid.Empty);
            queue.PlayerIds ??= new Dictionary<Guid, bool>();
            queue.PlayerIds.Add(Guid.Parse(id), true);
            await _firebase.CreateQueueAsync(queue);
            return Ok();
        }
    }
}
