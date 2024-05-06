using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using victors.Models.Helper;

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
        [HttpGet]
        public async Task<IActionResult> CreateAssessement()
        {
            var examCaches = await _db.ExamCache.ToListAsync();
            var students = await _db.promotions.Where(p => p.StudentId == examCaches.LastOrDefault().StudentId).ToListAsync();
            var course = await _db.Courses.FindAsync(examCaches.LastOrDefault().CourseId);
            
            AssessementJoinCourseJoinStudent assessementJoinCourseJoin = new AssessementJoinCourseJoinStudent();
            assessementJoinCourseJoin.course = course;
            assessementJoinCourseJoin.PromotedStudent = students.LastOrDefault();
            return View(assessementJoinCourseJoin);
        
        }

            [HttpPost]
        public async Task<IActionResult> CreateAssessement(AssessementJoinCourseJoinStudent data)
        {
            Assessement assessement = data.assessement;
            var examCaches = await _db.ExamCache.ToListAsync();
            var studentd = await _db.Students.FindAsync(examCaches.LastOrDefault().StudentId);
            var students = await _db.promotions.Where(p => p.StudentId == examCaches.LastOrDefault().StudentId).ToListAsync();
            if (examCaches.Any())
            {
                assessement.CourseId = examCaches.LastOrDefault().CourseId;
                assessement.StudentId = examCaches.LastOrDefault().StudentId;
                assessement.Section = students.LastOrDefault().Section;
                assessement.Stream = students.LastOrDefault().Stream;
                assessement.Year = students.LastOrDefault().Year;
                assessement.Grade = students.LastOrDefault().Grade;
                assessement.Term = students.LastOrDefault().Term;

              
                 await _academicaffairs.CreateAssessement(assessement, _db);
                _db.RemoveRange(examCaches);
                await _db.SaveChangesAsync();
                return RedirectToAction("GetAllCourse");
            }
            else
            {
          return View(data);
            }

        }
        [HttpGet("assessements")]


        public async Task<IActionResult> GetAllAssessements()
        {
            var data = await _academicaffairs.GetAllAssessements(_db);
            return View(data);
        }
        [HttpGet("assessements/{id}")]
        public async Task<IActionResult> getAllAssessements(int id)
        {
            var data = await _academicaffairs.getStudentAssessements(_db, id);
            return View(data);

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
                    StudentId = student.StudentId,
                    ExamSession = true,

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
                    StudentId = student.StudentId,
                    ExamSession = true,
                };
                await _db.ExamCache.AddAsync(exam);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("GetAllCourse");
        }

        [HttpGet("examCourse/{id}")]

        public async Task<IActionResult> StoreExamCourse(int id)
        {

            var examCaches = await _db.ExamCache.ToListAsync();
            var course = await _db.Courses.FindAsync(id);

            ExamCache examCache = new ExamCache();
            if (examCaches.Any())
            {
                examCache = examCaches.LastOrDefault();
                examCache.ExamSession = false;
                examCache.CourseId = course.CourseId;
                await _db.SaveChangesAsync(); 
                return RedirectToAction("CreateAssessement");

            }
            else
            {
                
                return RedirectToAction("GetAllCourse");

            }





        }




    }
}
