﻿using Microsoft.AspNetCore.Mvc;
using victors.Actions;
using victors.Models.Context;
using victors.Models.Helper;
using victors.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await studentActions.getStudents(_db);
            LookUpStudents lookUpStudents = new LookUpStudents();
            var response = new StudentIndex()
            {
                Students = result,
                LookUpStudents = lookUpStudents,
            };
           
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> SearchStudents(LookUpStudents lookUpStudents)
        {

            var data = await studentActions.SearchStudents(_db, lookUpStudents);

            return View(data);

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

            }        }

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
        [HttpGet("promote/{studentId}")]

        public async Task<IActionResult> PromoteStudent([FromRoute]int studentId)
        {
             odataManager = await studentActions.findStudent(studentId, _db);


            return View(odataManager.student);
        }
        [HttpPost("promote/{studentId}")]

        public async Task<IActionResult> PromoteStudent(Student studentdata)
        {
            var student = studentActions.StudenttoPromoted(studentdata);
            var promotedStudent = await studentActions.PromoteStudent(student, _db);

            return RedirectToAction("GetStudents");
        }

        [HttpGet("{id}/PaySchoolFees")]
        public async Task<IActionResult> PaySchoolFees(int id)
        {
            odataManager = await studentActions.findStudent(id, _db);
            var fees = await _db.schoolFees.Where(p => p.StudentId == id).ToListAsync();
            SchoolFees payment = new SchoolFees ();
            payment.Balance = fees.LastOrDefault().Balance;
                FeesJoinStudent feesJoinStudent = new (){ Student=odataManager.student,SchoolFees=payment};

            return View(feesJoinStudent);

        }
            [HttpPost("{id}/PaySchoolFees")]
        public async Task<IActionResult> PaySchoolFees(FeesJoinStudent data)
        {
            data.SchoolFees.StudentId = data.Student.StudentId;
           if (data.SchoolFees.StudentId!=0)
            {
              await studentActions.saveStudentSchFees(data.SchoolFees, _db);
                return RedirectToAction("GetStudents");

            }
            else
            {
                return View(data);
            }

        }
        //pay school requirement
        [HttpGet("payrequirements/{id}")]

        public async Task<IActionResult> PayRequirement([FromRoute] int id)
        {
            odataManager = await studentActions.findStudent(id, _db);
            var requirementsInDb = await _db.requirements.ToListAsync();
            StudentJoinRequirement studentJoinRequirement = new StudentJoinRequirement();
            studentJoinRequirement.Student=odataManager.student;
            studentJoinRequirement.Requirements=requirementsInDb;
            return View(studentJoinRequirement);
        
        }
            [HttpPost("payrequirements/{id}")]
        public async Task<IActionResult> PayRequirement(StudentJoinRequirement data)
        {
            var requirement = data.Requirement;
            requirement.StudentId=data.Student.StudentId;
          
            if (requirement == null)
            {
                return View(data);

            }
            else
            {
                var payment = await studentActions.payschoolRequirement(requirement, _db);
                return RedirectToAction("GetStudents");
            }
        }
        [HttpPost("newrequirement/{id}")]
        public async Task<IActionResult> NewSchoolRequirement(Requirement requirement)
        {
            var newRequirement = await studentActions.NewRequirement(requirement, _db);
            return RedirectToAction("GetStudents");
        }

        [HttpGet("newrequirement/{id}")]
        public async Task<IActionResult> NewSchoolRequirement()
        { return View();
        }

     
        [HttpGet("allrequirements")]
        public async Task<IActionResult> AllRequirements()
        {
            var requirementsInDb = await _db.requirements.ToListAsync();
            return RedirectToAction("GetStudents");
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

            return View(fees);
           
        }

        [HttpGet("{id}/studentPayments")]
        public async Task<IActionResult> StudentPayments([FromRoute] int id)
        {
            string[] items = new string[1];
            odataManager = await studentActions.findStudent(id, _db);

            var fees = await _db.schoolFees.Where(p => p.StudentId == id).ToListAsync();
            var requirements = await _db.RequirementsPayment.Where(p => p.StudentId == id).ToListAsync();

            StudentJoinFeesJoinRequirements studentjoinFeesJoinRequirements = new StudentJoinFeesJoinRequirements()
            {
                student = odataManager.student,
                requirementPayments = requirements,
                schoolFees = fees.OrderByDescending(k=>k.DateOfPayment),

            };
            return View(studentjoinFeesJoinRequirements);

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
            if (guardian.StudentId>0)
            {
                await _db.guardians.AddAsync(guardian);
                await _db.SaveChangesAsync();
                return RedirectToAction("AllGuardian");


            }

            return View(data);
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
            
                return View(data);
           
        }
        [HttpGet("requirents_paid")]
        public async Task<IActionResult> AllRequiremntsPayments()
        {
            var fees = await _db.RequirementsPayment.Where(p => p.Amount > 0).ToListAsync();
           
                return View(fees);
           
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


        [HttpGet("uniform/{id}")]
        public async Task<IActionResult> AddUniform()
        {
            var uniforms = await studentActions.GetUniformTypes(_db);
            StudentJoinUniform studentJoinUniform = new StudentJoinUniform();
            studentJoinUniform.uniforms = uniforms;

            return View(studentJoinUniform);
        }
            [HttpPost("uniform/{id}")]
        public async Task<IActionResult> AddUniform(StudentJoinUniform data)
        {
            var uniform = data.uniform;
            if (data.uniform.Cost>1000)
            {
                var result = await studentActions.NewUniform(uniform, _db);
                return RedirectToAction("GetStudents");

            }
            else
            {
                return View(data);
            }

        }
        // student pay uniform
        [HttpGet("payuniform/{id}")]
        public async Task<IActionResult> PayUniform(int id)
        {

            odataManager = await studentActions.findStudent(id, _db);
            List<Uniform> uniforms = await _db.Uniforms.ToListAsync();
            StudentJoinUniform studentJoinUniform = new ()
            {
                uniforms = uniforms,
                student = odataManager.student,

            };
            return View(studentJoinUniform);
        }
            [HttpPost("payuniform/{id}")]
        public async Task<IActionResult> PayUniform(StudentJoinUniform data)
        {
        var payment = data.uniformPayment;
            payment.StudentId = data.student.StudentId;
            payment.UniformId = data.uniform.UniformId;
            payment.StudentName =data.student.FirstName + " " + data.student.LastName;
            if (ModelState.IsValid)
            {
                var result = await studentActions.PayUniform(payment, _db);
                return RedirectToAction("GetStudents");

            }
            else { return View(data); }

        }
        [HttpGet("paiduniforms")]
        public async Task<IActionResult> GetAllPaidUniforms()
        {
            var result = await studentActions.GetUniformPayments(_db);
            return View(result);

        }
        [HttpGet("{id}/deletestudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            await studentActions.DeleteStudent(_db, id);
            return RedirectToAction("GetStudents");
        }

    }
}
