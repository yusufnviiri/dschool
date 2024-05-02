using victors.Models;
using victors.Models.Context;
using victors.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace victors.Actions
{

    public class AcademicAffair
    {
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> students { get; set; } = new List<Student>();
        
        public Course Course { get; set; }= new Course();

        public ICollection<Assessement> Assessements { get; set; } = new List<Assessement>();
        public ICollection<ReportCourse> reportCourses { get; set; } = new List<ReportCourse>();
        public Assessement Assessement { get; set; } = new ();
        public async Task<Course> CreateCourse(Course course,ApplicationDbContext _db)
        {
            course.stringToUpperCase(course.Name);
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
            return course;
        }
        public async Task<CourseJoinExamCache> GetAllCourse (ApplicationDbContext _db)
        {
           Courses = await _db.Courses.ToListAsync();
            List<ExamCache> examCaches= await _db.ExamCache.ToListAsync();
            ExamCache ExamCache = new ExamCache();
            if (examCaches.Any())
            {
                ExamCache = examCaches.LastOrDefault();
            }
            CourseJoinExamCache courseJoinExamCache = new CourseJoinExamCache()
            {
                courses = Courses,
                examCache = ExamCache,

            };

            return courseJoinExamCache;
        }
    

        public async Task<Assessement> CreateAssessement(Assessement assessement, ApplicationDbContext _db)
        {
            assessement.stringToUpperCase(assessement.PaceNumber);
            await _db.Assessements.AddAsync(assessement);
            await _db.SaveChangesAsync();
            PaceScores pacescores = new ()
            {
                courseId = assessement.CourseId,
                StudentId = assessement.StudentId,
                paceScore = assessement.PaceScore,
                term = assessement.Term,
            };
            await _db.PaceScores.AddAsync(pacescores);
            await _db.SaveChangesAsync();
            return assessement;
        }
        
        public async Task<ICollection<AssessementJoinCourseJoinStudent>> GetAllAssessements(ApplicationDbContext _db)
        {
            Courses = await _db.Courses.ToListAsync();
            students = await _db.Students.ToListAsync();
            Assessements = await _db.Assessements.ToListAsync();
            var data = (from item in Assessements
                        select new AssessementJoinCourseJoinStudent
                        {
                            student = students.Where(k => k.StudentId == item.StudentId).FirstOrDefault(),
                            assessement = item,
                            course = Courses.Where(k => k.CourseId == item.CourseId).FirstOrDefault(),
                        }).ToList();
            return data;
            
        }
        public async Task<IEnumerable<ReportCourse>> getStudentAssessements(ApplicationDbContext _db,int Id)
        {
            var student= await _db.Students.Where(k=>k.StudentId==Id).FirstOrDefaultAsync();
            List<ReportCourse> finalData = new List<ReportCourse>();
            Courses = await _db.Courses.ToListAsync();
            var assesmentList = await _db.Assessements.ToListAsync();

            Assessements = assesmentList.Where(p => p.StudentId == Id).ToList();
            if (Assessements.Count > 0)
            {
                var datamine = await _db.Assessements.Where(k => k.StudentId == Id).GroupBy(h => h.CourseId).ToListAsync();
                var reportCourses = (from item in Courses
                                     select new ReportCourse
                                     {
                                         course = item,
                                         assessements = Assessements.Where(k => k.CourseId == item.CourseId).ToList(),
                                         score = 0
                                     });
                var scores = await _db.PaceScores.Where(k => k.StudentId == Id).ToListAsync();
                var av = scores.Average(p => p.paceScore);
                var data = (from item in datamine
                            select new AssessementSumary
                            {
                                course = Courses.Where(k => k.CourseId == item.FirstOrDefault().CourseId).FirstOrDefault(),
                                assessements = Assessements.Where(k => k.CourseId == item.FirstOrDefault().CourseId).ToList(),
                                Final = (item.Sum(k => k.PaceScore)) / item.Count()
                            }).ToList();
            
                foreach (var item in reportCourses)
                {
                    if (data.Where(k => k.course.CourseId == item.course.CourseId).FirstOrDefault() == null)
                    {
                        item.score = 0;
                    }
                    else
                    {
                        item.score = data.Where(k => k.course.CourseId == item.course.CourseId).FirstOrDefault().Final;

                    }
                    finalData.Add(item);
                }

            }

                if (finalData.Count > 0)
                {
                    return finalData;
                }
                else
                {
                    return reportCourses;
                }
            
          

        }

    
    }
}
