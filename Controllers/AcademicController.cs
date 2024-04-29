using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var data = await _academicaffairs.CreateCourse(course, _db);
            return  View();
        }
        [HttpGet("GetAllCourse")]


        public async Task<IActionResult> GetAllCourse()
        {
            var data = await _academicaffairs.GetAllCourse(_db);
            return Ok(data);
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


    }
}
