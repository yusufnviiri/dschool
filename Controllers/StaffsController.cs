using victors.Actions;
using victors.Models.Context;
using victors.Models.Helper;
using victors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace victors.Controllers
{


    public class StaffsController : Controller
    {
        public StaffsController(ApplicationDbContext db)
        {
            _db = db;
        }



        private readonly ApplicationDbContext _db;
        public OdataManager odataManager { get; set; } = new();
        public StudentActions studentActions { get; set; } = new();
        public StaffActions staffActions { get; set; } = new();
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
            [HttpPost]

        public async Task<IActionResult> Create(Staff staff)
        {
            if (ModelState.IsValid)            
            {
                var newStaff = await staffActions.CreateStaff(_db, staff);
                return RedirectToAction("GetAllStaff");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var result = await staffActions.GetAllStaff(_db);

            return View(result);
        }

        [HttpGet("Staff/{id}")]

        public async Task<IActionResult>Staff(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var result = await staffActions.GetStaff(_db, id);
                return View(result);
            }

        }
        [HttpGet("update/{id}")]
        
        public async Task<IActionResult> UpdateStaff(int id)
        {
            var staff = await _db.staffs.FindAsync(id);
            return View(staff);
        }

            [HttpPost("update/{id}")]

        public async Task<IActionResult> UpdateStaff(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }
            else
            {
                var result = await staffActions.UpdateStaff(_db, staff);
                return RedirectToAction("GetAllStaff");
            }

        }

        [HttpGet("wages/{id}")]

        public async Task<IActionResult> PayWages(int id)
        {
            var staff = await _db.staffs.FindAsync(id);
            StaffJoinWage staffJoinWage= new StaffJoinWage();
            staffJoinWage.Staff = staff;

            return View(staffJoinWage);
        }

            [HttpPost("wages/{id}")]

        public async Task<IActionResult> PayWages(StaffJoinWage data)
        {
            data.Wage.StaffId=data.Staff.StaffId;

            if (data.Wage == null)
            {
                return View(data);
            }
            else
            {
                var newStaffWage = await staffActions.payStaffWages(_db, data.Wage);
                return RedirectToAction("GetAllStaff");
            }
        }
        [HttpGet("staff_wages")]

        public async Task<IActionResult> GetAllWages()
        {
            // var wages = await _db.wages.Include(k => k.Staff).GroupBy(h => h.PayMonth).ToListAsync();
            var wages = await _db.wages.Include(k => k.Staff).ToListAsync();
            return View(wages);
        }


    }

}
