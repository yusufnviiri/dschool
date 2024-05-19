using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using victors.Models.Helper;

namespace victors.Controllers
{


    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly Affairs _affairs = new Affairs();
        public OdataManager odataManager { get; set; } = new();
        public StudentActions studentActions { get; set; } = new();
        private readonly UserActions _userActions = new();
        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("feedback/{id}")]
        public async Task<IActionResult> CreateNoticeFromWeb()
        {
            return View();
        }
        [HttpPost("feedback/{id}")]
        public async Task<IActionResult> CreateNoticeFromWeb(Notice notice)
        {
            notice.DueDate = DateTime.Now;
            notice.Postedby = "Web FeedBack";

            notice.SetDate(notice.DueDate);
            var postedNotice = await _affairs.postNotice(_db, notice);
            return Redirect("https://victorsnestchristianschool.net/");
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(User user)
        //{

        //    var result = await _userActions.CreateUser(_db, user);
        //    if (result.RequestConfirmation)
        //    {
        //        return Ok("Successfull User Created ");
        //    }
        //    else
        //    {
        //        if (result.ResponseData.ToLower().Contains("user already used"))
        //            return Ok("user already used.kindly select another user name");
        //        return NotFound();
        //    }
        //}
        //[HttpGet, Authorize]
        //public async Task<IActionResult> Index()
        //{
        //    var users = await _userActions.AllUsers(_db);
        //    if (users.Any())
        //    {
        //        return Ok(users);
        //    }
        //    else
        //    {
        //        return Ok("No user Added");
        //    }
        //}



    }

}
