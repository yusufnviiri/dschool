using victors.Models;
using victors.Models.Context;
using victors.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace victors.Actions
{
    public class StudentActions
    {
        public Student student { get; set; } = new ();
        public Uniform uniform { get; set; } = new();
        public ICollection<Uniform> uniformList { get; set; } = new List<Uniform>();
        public Promotion promotion { get; set; } = new ();
        public Requirement requirement { get; set; } = new();
        public RequirementPayment requirementPayment { get; set; }= new();
        public SchoolFees schFees { get; set; }= new ();
        public OdataManager odataManager { get; set; } = new();
        public ICollection<SchoolFees> SchoolFeesList { get; set; } = new List<SchoolFees> ();
        public ICollection<RequirementPayment> requirementPayments { get; set; } = new List<RequirementPayment> ();
        public ICollection<Promotion> promotionList { get; set; }
        public ICollection<Student> students { get; set; } = new List<Student> ();
        public async Task<List<Student>> getStudents(ApplicationDbContext _db)
        {   promotionList = await _db.promotions.ToListAsync(); 
            students = await _db.Students.ToListAsync();

            var data = (from promoted  in promotionList join stdnt in students on promoted.StudentId equals stdnt.StudentId  select new Student
            {
                FirstName = stdnt.FirstName,
                LastName = stdnt.LastName,
                StudentId = stdnt.StudentId,
                SchoolFees = promoted.SchoolFees,
                Term = promoted.Term,
                Grade = promoted.Grade,
                Year = promoted.Year,
                Stream = promoted.Stream,
                Section = promoted.Section,
                Status = promoted.Status,
                birthDateString=stdnt.birthDateString,
                Gender= stdnt.Gender,
                Village=stdnt.Village,
                District=stdnt.District,
                Email=stdnt.Email,
            }).ToList();
            return data;
        }
        public async Task<Student> createSudents(Student student,ApplicationDbContext _db)
        {  

            student.AgeCalc(student.Birthdate);
            student.setDateString(student.Birthdate);
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            students = await _db.Students.ToListAsync();
            var std = students.LastOrDefault();
            var stdFees = new SchoolFees
            {
                StudentId = std.StudentId,
                Balance = std.SchoolFees,
                Reason = " student registration"
            };
            await _db.schoolFees.AddAsync(stdFees);
            await _db.SaveChangesAsync();
            var acJorny = new AcademicJouney
            {
                Grade = std.Grade,
                Term = std.Term,
                Section = std.Section,
                Status = std.Status,
                Stream = std.Stream,
                SchoolFees = std.SchoolFees,
                StudentId = std.StudentId,
                Year = std.Year
            };
            await _db.academicJouneys.AddAsync(acJorny);
            await _db.SaveChangesAsync();
            var stdPro = new Promotion
            {
                Grade = std.Grade,
                Term = std.Term,
                Section = std.Section,
                Status = std.Status,
                Stream = std.Stream,
                SchoolFees = std.SchoolFees,
                StudentId = std.StudentId,
                Year = std.Year,
                Village = std.Village,
            };
            await _db.promotions.AddAsync(stdPro);
            await _db.SaveChangesAsync();

//add uniform 
if (student.fullUniform == true)
            {
                // find student
                 students = await _db.Students.ToListAsync();
                var lastStudent = students.LastOrDefault();
                var uniform = await _db.Uniforms.Where(k => k.AcademicLevel == lastStudent.Section).Where(m => m.Item.ToLower() == "full uniform").ToListAsync();
                var uniformPayment = new UniformPayment();
                if(uniform.Any()){
                    uniformPayment.Amount = student.charge;
                    uniformPayment.UniformDetails = $"full uniform for {student.Section}";
                    uniformPayment.StudentId = lastStudent.StudentId;
                    uniformPayment.UniformId = uniform.FirstOrDefault().UniformId;
                    uniformPayment.RecievedBy = "admin";
                    uniformPayment.StudentName = $"{lastStudent.LastName} {lastStudent.FirstName} ";
                    await _db.UniformPayments.AddAsync(uniformPayment);
                    await _db.SaveChangesAsync();
                } else
                {
                    Uniform studentUniform = new Uniform
                    {
                        Item = "full uniform",
                        Cost = student.charge,
                        AcademicLevel = student.Section,
                    };
                    await _db.Uniforms.AddAsync(studentUniform);
                    await _db.SaveChangesAsync();
                    uniformList = await _db.Uniforms.ToListAsync();
                    var newUniform = uniformList.LastOrDefault();
                    var lastUniform = await _db.Uniforms.ToListAsync();

                    uniformPayment.Amount = student.charge;
                    uniformPayment.UniformDetails = $"full uniform for {student.Section}";
                    uniformPayment.StudentId = lastStudent.StudentId;
                    uniformPayment.UniformId =lastUniform.FirstOrDefault().UniformId;
                    uniformPayment.RecievedBy = "admin";
                    uniformPayment.StudentName = $"{lastStudent.LastName} {lastStudent.FirstName} ";
                    await _db.UniformPayments.AddAsync(uniformPayment);
                    await _db.SaveChangesAsync();
                }
                ;
            }
            return student;  
        }
      
        public async Task<Promotion> PromoteStudent(Promotion student, ApplicationDbContext _db)
        {
            promotionList = await _db.promotions.Where(p => p.StudentId == student.StudentId).ToListAsync();
            if (promotionList.Any())
            {
                // update class status of the student
              var  promotedstudent = promotionList.LastOrDefault();
                var chnge = promotedstudent.PromotionId;
                promotedstudent.Grade = student.Grade;
                promotedstudent.Term = student.Term;
                promotedstudent.Section = student.Section;
                promotedstudent.Status = student.Status;
                promotedstudent.Stream = student.Stream;
                promotedstudent.SchoolFees = student.SchoolFees;
                promotedstudent.RegistrationDate = student.RegistrationDate;
                        promotedstudent.Village = student.Village;
                await _db.SaveChangesAsync();
            }
            else
            {
                await _db.promotions.AddAsync(student);
                await _db.SaveChangesAsync();            }
            // update school fees Account
            var std = await _db.Students.FindAsync(student.StudentId);
            var prevFees = await _db.schoolFees.Where(f => f.StudentId == student.StudentId).ToListAsync();
            var stfees = prevFees.LastOrDefault();
            var stdFees = new SchoolFees
            {
                StudentId = student.StudentId,
                Balance = student.SchoolFees + stfees.Balance,
                Reason = " student promotion"
            };
            await _db.schoolFees.AddAsync(stdFees);
            await _db.SaveChangesAsync();
            var acJorny = new AcademicJouney
            {
                Grade = student.Grade,
                Term = student.Term,
                Section = student.Section,
                Status = student.Status,
                Stream = student.Stream,
                SchoolFees = student.SchoolFees,
                StudentId = student.StudentId,
                        };
            await _db.academicJouneys.AddAsync(acJorny);
            await _db.SaveChangesAsync();
            return student;
        }
       public async Task<OdataManager> findStudent(int id,ApplicationDbContext _db){  
                 student = await _db.Students.FindAsync(id);
                 promotionList = await _db.promotions.Where(p => p.StudentId == student.StudentId).ToListAsync();
                promotion = promotionList.FirstOrDefault();
                student.Grade = promotion.Grade;
                student.Village = promotion.Village;
                student.Term = promotion.Term;
                student.Stream = promotion.Stream;
                student.Section = promotion.Stream;
                student.Year = promotion.Year;
            student.Status = promotion.Status;
                student.SchoolFees = promotion.SchoolFees;            
                var schFees = await _db.schoolFees.Where(p => p.StudentId == id).ToListAsync();
                odataManager.schoolFees = schFees.LastOrDefault();
                odataManager.student = student;
                return odataManager;
        }
        public async Task<SchoolFees> saveStudentSchFees(SchoolFees schoolFees,ApplicationDbContext _db)
        {
            SchoolFeesList = await _db.schoolFees.Where(p => p.StudentId == schoolFees.StudentId).ToListAsync();
            schFees = SchoolFeesList.LastOrDefault();
            if (schFees != null)
            {
                schoolFees.Balance = schFees.Balance;
            }             
                schoolFees.Balance -= schoolFees.Amount;            
                schoolFees.AmountDue = schoolFees.Balance;
                await _db.schoolFees.AddAsync(schoolFees);
                await _db.SaveChangesAsync();
                return schoolFees;        }
        public async Task<Requirement> payschoolRequirement(Requirement requirement,ApplicationDbContext _db)
        {
            //requiremnts paid 
            var flag = requirement.Balance;
            var reqPayment = await _db.RequirementsPayment.Where(k => k.RequirementId == requirement.RequirementId).ToListAsync();
            //get requirent and exact student
            if (reqPayment.Any()) {
            var studentpaidRequiremnt = (from item in reqPayment where item.StudentId == requirement.StudentId select item).ToList();
                if(studentpaidRequiremnt.Any()){
                    var prevPayment = studentpaidRequiremnt.LastOrDefault();
                    requirement.Balance =  prevPayment.Balance- requirement.Amount;
                }
                else
                {
                    var newstudentpaidRequiremnt = (from item in reqPayment where item.StudentId == 0 select item).ToList();
                    if (newstudentpaidRequiremnt.Any())
                    {
                        requirement.Balance =   newstudentpaidRequiremnt.LastOrDefault().Balance- requirement.Amount;
                    }
                }
            }
            student = await _db.Students.FindAsync(requirement.StudentId);
            var reqInDb = await _db.requirements.FindAsync(requirement.RequirementId);
            requirementPayment = new RequirementPayment
            {
                StudentId = requirement.StudentId,
                Amount = requirement.Amount,
                Balance = requirement.Balance,
                Reason = requirement.Reason,
                RequiremntName = reqInDb.Name,
                PaidBy = $"{student.FirstName}" + " " + $"{student.LastName}",
                // PaidBy = student.FirstName + student.LastName,                
                RecievedBy = requirement.RecievedBy,
                RequirementId = requirement.RequirementId,
                Term = requirement.Term,
                Grade = requirement.Grade,
            };

            await _db.RequirementsPayment.AddAsync(requirementPayment);
            await _db.SaveChangesAsync();          
            return (requirement);
        }
        public async Task<Requirement> NewRequirement(Requirement requirement,ApplicationDbContext _db)
        {
            requirement.Balance = requirement.Charge;            
            await _db.requirements.AddAsync(requirement);
            await _db.SaveChangesAsync();
           requirementPayment.Balance= requirement.Balance;
           requirementPayment.RequirementId = requirement.RequirementId;
           requirementPayment.RequiremntName = requirement.Name;
           requirementPayment.Reason = requirement.Reason;
           await _db.RequirementsPayment.AddAsync(requirementPayment);
           await _db.SaveChangesAsync();
            return requirement;
        }

        public async Task<Uniform> NewUniform(Uniform uniform, ApplicationDbContext _db)
        {

            var dbUniforms = await _db.Uniforms.Where(k=>k.Item.ToLower() == uniform.Item.ToLower()).ToListAsync();
            if (dbUniforms.Any())
            {
                return uniform;
            } else
            {
                await _db.Uniforms.AddAsync(uniform);
                await _db.SaveChangesAsync();
                return uniform;
            }
            // check if uniform item exists
        }
        public async Task<UniformPayment> PayUniform(UniformPayment payment, ApplicationDbContext _db)
        {
            var uniforms = await _db.Uniforms.Where(p => p.UniformId == payment.UniformId).ToListAsync();
            uniform = uniforms.LastOrDefault();

            payment.SetValueAttributes(uniform);
                await _db.UniformPayments.AddAsync(payment);
                await _db.SaveChangesAsync();
                return payment;           
        }
        public async Task<ICollection<Uniform>> GetUniformTypes(ApplicationDbContext _db)
        {
            var uniforms = await _db.Uniforms.ToListAsync();
            return uniforms;
            // check if uniform item exists
        }
        public async Task<ICollection<StudentJoinUniform>> GetUniformPayments(ApplicationDbContext _db)
        {
            var paidUniforms = await _db.UniformPayments.ToListAsync();
            uniformList = await _db.Uniforms.ToListAsync();
            students = await _db.Students.ToListAsync();

            var uniforms = await _db.Uniforms.ToListAsync();
            var data = (from item in paidUniforms
                        select new StudentJoinUniform
                        {
                            student = students.Where(k => k.StudentId == item.StudentId).FirstOrDefault(),
                            uniform = uniformList.Where(k => k.UniformId == item.UniformId).FirstOrDefault(),
                            uniformPayment = item,


                        }).ToList();
            return data;

            // check if uniform item exists
        }
        public Promotion StudenttoPromoted(Student student)
        { var promoted=new Promotion();

            promoted.StudentId = student.StudentId;
            promoted.contact=student.Contact;
            promoted.Section=student.Section;
            promoted.Stream=student.Stream;
            promoted.SchoolFees=student.SchoolFees;
            promoted.Grade=student.Grade;
            promoted.Village= student.Village;
            promoted.Status=student.Status;
            promoted.Term=student.Term;        

            return promoted;
        }

    }
}
