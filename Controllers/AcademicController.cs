using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace victors.Controllers
{
    public class AcademicController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Affairs _affairs = new Affairs();
        private readonly AcademicAffair _academicaffairs = new();
        public AcademicController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("CreateCourse")]
        public async Task<IActionResult> CreateCourse()
        {
            return View();
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var data = await _academicaffairs.CreateCourse(course, _db);
            return RedirectToAction("GetAllCourse");

        }
        [HttpGet("GetAllCourse")]


        public async Task<IActionResult> GetAllCourse()
        {
            var data = await _academicaffairs.GetAllCourse(_db);
            return View(data);
        }

        [HttpPost("assessement")]
        public async Task<IActionResult> CreateAssessement(Assessement assessement)
        {
            var data = await _academicaffairs.CreateAssessement(assessement, _db);
            return Ok(data);
        }
        [HttpGet("assessements")]


        public async Task<IActionResult> getAllAssessements()
        {
            var data = await _academicaffairs.GetAllAssessements(_db);
            return Ok(data);
        }
        [HttpGet("assessements/{id}")]
        public async Task<IActionResult> getAllAssessements(int id)
        {
            var data = await _academicaffairs.getStudentAssessements(_db, id);
            return Ok(data);

            //var data = await _db.Assessements.Where(k=>k.StudentId==id).GroupBy(h=>h.CourseId).ToListAsync();
            //return Ok(data);
        }
        [HttpGet("examstudent/{id}")]

        public async Task<IActionResult> StoreExamStudent(int id)
        {
            var student = await _db.Students.FindAsync(id);
            var examCache = await _db.ExamCache.ToListAsync();
            if (examCache.Count == 0)
            {
                ExamCache exam = new()
                {
                    Student = student,
                };
            await _db.ExamCache.AddAsync(exam);
                await _db.SaveChangesAsync();
            }
            else
            {
                _db.ExamCache.RemoveRange(examCache);
                await _db.SaveChangesAsync();
                ExamCache exam = new()
                {
                    Student = student,
                };
                await _db.ExamCache.AddAsync(exam);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("GetAllCourse");
        }

        [HttpGet("examCourse/{id}")]

        public async Task<IActionResult> StoreExamCourse(int id)
        {
            var examCache = await _db.ExamCache.ToListAsync();
            var course = await _db.Courses.FindAsync(id);
            examCache.LastOrDefault().Course = course;
            await _db.SaveChangesAsync(); return RedirectToAction("CreateAssessement");


        }




    }
}
