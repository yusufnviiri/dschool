using victors.Actions;
using victors.Models.Context;
using victors.Models.Helper;
using victors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace victors.Controllers
{



    public class SchoolAffairsController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly Affairs _affairs = new Affairs();
        public SchoolAffairsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PettyCash pettyCash)
        {
            if (ModelState.IsValid)
            {
                var postedTrans = await _affairs.AddPettyCash(_db, pettyCash);
                return Ok(postedTrans);

            }
            else
            {
                return BadRequest();

            }

        }
        [HttpGet("expenses")]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _affairs.GetPettyCash(_db);
            return Ok(expenses);
        }
        [HttpGet("notice/{id}")]
        public async Task<IActionResult> CreateNotice(int id)
        {
            return View();
        }



            [HttpPost("notice/{id}")]
        public async Task<IActionResult> CreateNotice(Notice notice)
        {
            if (ModelState.IsValid)
            {
                notice.SetDate(notice.DueDate);
                var postedNotice = await _affairs.postNotice(_db, notice);
                return RedirectToAction("GetNotices");

            }
            else
            {
                return View(notice);

            }

        }
        [HttpGet("cashflow")]
        public async Task<IActionResult> CashFlows()
        {
            var flows = await _affairs.CashFlows(_db);
            var notices = await _affairs.getAllNotice(_db);
            NoticeJoinCashFlow data = new()
            {
                Notices = notices,
                CashFlows = flows
            };

            return Ok(data);
        }

        [HttpGet("notices")]
        public async Task<IActionResult> GetNotices()
        {
            var notices = await _affairs.getAllNotice(_db);
            return View(notices);
        }
        [HttpGet("{id}/notice")]
        public async Task<IActionResult> FindNotices(int id)
        {
            var notice = await _affairs.FindNotice(_db, id);
            return Ok(notice);
        }
        [HttpPost("asset")]
        public async Task<IActionResult> CreateAsset(Asset asset)
        {
            if (ModelState.IsValid)
            {
                var postedAsset = await _affairs.postAsset(_db, asset);
                return Ok(postedAsset);

            }
            else
            {
                return BadRequest();

            }

        }
        [HttpGet("assets")]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _affairs.getAllAssets(_db);
            return Ok(assets);
        }
        [HttpGet("{id}/asset")]
        public async Task<IActionResult> FindAsset(int id)
        {
            var asset = await _affairs.FindAsset(_db, id);
            return Ok(asset);
        }


        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _affairs.GetallBooks(_db);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Createbook()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Createbook(Book book)
        {
            if (ModelState.IsValid)
            {
                var newBook = await _affairs.CreateBook(_db, book);
                return RedirectToAction("GetAllBooks");

            }
            else
            {
                return BadRequest();

            }

        }

    }



}
