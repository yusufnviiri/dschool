using Microsoft.AspNetCore.Mvc;
using victors.Actions;
using victors.Models.Context;
using victors.Models.Helper;
using victors.Models;
using Microsoft.EntityFrameworkCore;

namespace victors.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OdataManager odataManager { get; set; } = new();
        public StudentActions studentActions { get; set; } = new();
        public StudentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // [HttpGet, Authorize(Roles ="admin,burser")]
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await studentActions.getStudents(_db);

            return View(result);
        }



        [HttpGet]
        public async Task<IActionResult> CreateStudent()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {

                var newstudent = await studentActions.createSudents(student, _db);
                return RedirectToAction("GetStudents");
            }
            else
            {
                return View();

            }



        }

        [HttpGet("Student/{id}")]
        //[HttpGet("{id}"), Authorize(Roles = "parent,admin")]

        public async Task<IActionResult> Student(int id)
        {
            if (id == 0)
            {
                return BadRequest($"{id} does not exist");
            }
            else
            {
                odataManager = await studentActions.findStudent(id, _db);
                return View(odataManager);
            }

        }

        [HttpPost("{id}/promote")]

        public async Task<IActionResult> PromoteStudent(Promotion student)
        {
            var promotedStudent = await studentActions.PromoteStudent(student, _db);


            return Ok(promotedStudent);
        }
        [HttpPost("{id}/schoolfees")]
        public async Task<IActionResult> paySchoolFees(SchoolFees schoolFees)
        {
            var Fees = new SchoolFees();
            if (schoolFees == null)
            {
                return BadRequest(schoolFees);
            }
            else if (ModelState.IsValid)
            {
                Fees = await studentActions.saveStudentSchFees(schoolFees, _db);
            }

            return Ok(Fees);
        }
        [HttpPost("{id}/requirements")]
        public async Task<IActionResult> payRequirement(Requirement requirement)
        {
            if (requirement == null)
            {
                return BadRequest("No Input");

            }
            else
            {
                var payment = await studentActions.payschoolRequirement(requirement, _db);
                return Ok(payment);
            }
        }
        [HttpPost("newRequirement")]
        public async Task<IActionResult> NewSchoolRequirement(Requirement requirement)
        {
            var newRequirement = await studentActions.NewRequirement(requirement, _db);
            return Ok(newRequirement);
        }
        [HttpGet("allrequirements")]
        public async Task<IActionResult> AllRequirements()
        {
            var requirementsInDb = await _db.requirements.ToListAsync();
            return Ok(requirementsInDb);
        }
        [HttpGet("{id}/requirements")]
        public async Task<IActionResult> requirements([FromRoute] int id)
        {
            string[] items = new string[1];
            var requirements = await _db.RequirementsPayment.Where(p => p.StudentId == id).ToListAsync();
            return Ok(requirements);

        }
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> Payments([FromRoute] int id)
        {
            string[] items = new string[1];
            var fees = await _db.schoolFees.Where(p => p.StudentId == id).ToListAsync();
            if (fees.Any())
            {
                return Ok(fees);
            }
            else
            {
                return Ok(items);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CreateGuardian(int id)
        {
            odataManager = await studentActions.findStudent(id, _db);
            StudentJoinGuardian studentJoinGuardian = new ();
            studentJoinGuardian.Student = odataManager.student;
            return View(studentJoinGuardian);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateGuardian(StudentJoinGuardian data)
        {var guardian = data.Guardian;
            guardian.StudentId = data.Student.StudentId;
            if (ModelState.IsValid)
            {
                await _db.guardians.AddAsync(guardian);
                await _db.SaveChangesAsync();
                return RedirectToAction("GetStudents");

            }

            return RedirectToAction("AllGuardian");
        }

        [HttpGet("{id}/guardians")]
        public async Task<IActionResult> GetGuardians([FromRoute] int id)
        {
            string[] items = new string[1];
            var guardians = await _db.guardians.Where(p => p.StudentId == id).ToListAsync();
            if (guardians.Any())
            {
                return Ok(guardians);
            }
            else
            {
                return Ok(items);
            }
        }



        [HttpGet("guardians")]
        public async Task<IActionResult> AllGuardian()
        {
            var guardians = await _db.guardians.ToListAsync();
           
               return View(guardians);
          
        }
        [HttpGet("schoolfees")]
        public async Task<IActionResult> AllFeesPayments()
        {
            var students = await _db.Students.ToListAsync();
            var fees = await _db.schoolFees.Where(a => a.Amount > 0).GroupBy(k => k.StudentId).ToListAsync();
            var postedFees = (from item in fees
                              select new SchoolFees
                              {
                                  Amount = item.Sum(s => s.Amount),
                                  StudentId = item.FirstOrDefault().StudentId,
                                  Balance = item.Sum((s) => s.Balance),
                                  AmountDue = item.Sum(s => s.AmountDue),
                                  SchoolFeesId = item.FirstOrDefault().SchoolFeesId,
                              }).ToList();
            var data = (from item in postedFees
                        select new FeesJoinStudent
                        {
                            SchoolFees = item,
                            Student = students.Where(k => k.StudentId == item.StudentId).ToList().FirstOrDefault()
                        }).ToList();
            if (data.Any())
            {
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }
        [HttpGet("requirentsPaid")]
        public async Task<IActionResult> AllRequiremntsPayments()
        {
            var fees = await _db.RequirementsPayment.Where(p => p.Amount > 0).ToListAsync();
            if (fees.Any())
            {
                return Ok(fees);
            }
            else
            {
                return Ok();
            }
        }
        [HttpGet("uniforms")]
        public async Task<IActionResult> GetAllUniforms()
        {
            var uniforms = await studentActions.GetUniformTypes(_db);
            if (uniforms.Any())
            {
                return Ok(uniforms);
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("uniform")]
        public async Task<IActionResult> AddUniform(Uniform uniform)
        {
            if (ModelState.IsValid)
            {
                var result = await studentActions.NewUniform(uniform, _db);
                return Ok(result);

            }
            else { return BadRequest(); }

        }
        [HttpPost("payuniform")]
        public async Task<IActionResult> PayUniform(UniformPayment payment)
        {
            //var uniform = await _db.Uniforms.Where(k => k.UniformId == payment.Uniform.UniformId).ToListAsync();
            //payment.Uniform = uniform.FirstOrDefault();
            if (ModelState.IsValid)
            {
                var result = await studentActions.PayUniform(payment, _db);
                return Ok(result);

            }
            else { return BadRequest(); }

        }
        [HttpGet("paiduniforms")]
        public async Task<IActionResult> GetAllPaidUniforms()
        {
            var result = await studentActions.GetUniformPayments(_db);
            return Ok(result);

        }
    }
}
