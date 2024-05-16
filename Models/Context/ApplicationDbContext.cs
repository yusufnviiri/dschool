using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using victors.Configuration;

namespace victors.Models.Context
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {



        //configure context
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{

        //}
        public ApplicationDbContext(DbContextOptions options)
  : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());



            //});
            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    UserId = 1,
            //    UserName = "admin",
            //    Password = "admin",
            //    Role = "admin",
            //}, new User
            //{
            //    UserId = 2,
            //    UserName = "burser",
            //    Password = "burser",
            //    Role = "burser"
            //}, new User
            //{
            //    UserId = 3,
            //    UserName = "parent",
            //    Password = "parent",
            //    Role = "parent"
            //});

        }


        public DbSet<Student> Students { get; set; }
        public DbSet<AcademicJouney> academicJouneys { get; set; }
        public DbSet<Guardian> guardians { get; set; }
        public DbSet<SchoolFees> schoolFees { get; set; }
        public DbSet<Requirement> requirements { get; set; }
        public DbSet<Promotion> promotions { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<RequirementPayment> RequirementsPayment { get; set; }
        public DbSet<PettyCash> pettyCashes { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Wage> wages { get; set; }
        public DbSet<PasswordChange> PasswordChanges { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Asset> assets { get; set; }
        public DbSet<Uniform> Uniforms { get; set; }
        public DbSet<UniformPayment> UniformPayments {  get; set; }  
        public DbSet<Assessement> Assessements { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PaceScores> PaceScores { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamCache> ExamCache { get; set; }
        //seeding data
    

    }
}
