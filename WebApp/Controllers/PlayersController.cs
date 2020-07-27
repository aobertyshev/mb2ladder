using Microsoft.AspNetCore.Mvc;
using Shared.Contexts;

namespace WebApp.Controllers
{
    public class PlayersController : ControllerBase
    {
        private readonly LadderDbContext _db;
        public PlayersController(LadderDbContext db)
        {
            _db = db;
        }

        public IActionResult List()
        {
            return Ok(_db.Users);
        }
    }
}
