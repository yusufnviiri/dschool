using victors.Actions;
using victors.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace victors.Controllers
{

    public class AuditTrailsController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly UserActions _userActions = new();
        public AuditTrailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> AuditTrails()
        {
            var users = await _userActions.AllAuditTrails(_db);
            if (users.Any())
            {
                return Ok(users);
            }
            else
            {
                return Ok("No data found");
            }

        }

    }
}

