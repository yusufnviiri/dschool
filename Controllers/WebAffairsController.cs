using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Notice;
using System.Diagnostics;
using victors.Actions;
using victors.Models;
using victors.Models.Context;
using victors.Models.Helper;
using static System.Collections.Specialized.BitVector32;

namespace victors.Controllers
{
    public class WebAffairsController : Controller
    {
        public StudentActions studentActions { get; set; } = new();
        private readonly ApplicationDbContext _db;
        public WebAffairsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("webapplication")]
        public async Task<IActionResult> WebApplication()
        {
            return View();
        }
        [HttpPost("webapplication")]
        public async Task<IActionResult> WebApplication(StudentFromWebsite webstudent)
        {

            await studentActions.CreateWebStudent(webstudent, _db);

            return Redirect("https://victorsnestchristianschool.net/");
        }
        [HttpGet]
        public async Task<IActionResult> PendingApplications()
        {
            var pendingStudents = await _db.studentsFromWebsite.ToListAsync();
            return View(pendingStudents);
        }
      

        public async Task<IActionResult> RejectApplication(int id)
        {
            await studentActions.RejectWebStudent(id, _db);           
            return RedirectToAction("PendingApplications");
        }

        [HttpGet]
        public async Task<IActionResult> CreateStudentFromWeb(int id)
        {
            var student = await _db.studentsFromWebsite.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudentFromWeb(StudentFromWebsite studentData)
        {
            var newStudent = studentData;
            Student student = new()
            {
            FirstName=studentData.FirstName,
            LastName=studentData.LastName,
            Term=studentData.Term,
            Section=studentData.Section,
            Status=studentData.Status,
            Stream=studentData.Stream,
            Grade=studentData.Grade,
            SchoolFees=studentData.SchoolFees,
            Nationality=studentData.Nationality,
            Village=studentData.Village,
            Religion=studentData.Religion,
            Birthdate=studentData.Birthdate,
            fullUniform=studentData.fullUniform,
            charge=studentData.charge,
            District=studentData.District,
            Contact=studentData.Contact,
            Age=studentData.Age,
            Gender=studentData.Gender,
            };
                      await studentActions.createSudents(student,_db);
            await studentActions.RejectWebStudent(studentData.StudentFromWebsiteId, _db);
            return RedirectToAction("PendingApplications");
        }
    }
}
