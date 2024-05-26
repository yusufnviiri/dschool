using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Notice;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Macs;
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
        string DIRECTOR_ID = "51009a63-58e3-45f0-b9b3-8442c0c3d847";
          string ROLE_ID = "253a7f33-4741-4d5d-90ec-c7353f66403d";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Director",
                NormalizedName = "DIRECTOR",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });
            var appUser = new User
            {
                Id = DIRECTOR_ID,
                Email = "director@victors",
                EmailConfirmed = false,
                FirstName = "Director",
                LastName = "Director",
                Function="Director",
                UserName = "Director@victors",
            NormalizedUserName ="DIRECTOR@VICTORS"
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "director@victors");

            //seed user
            modelBuilder.Entity<User>().HasData(appUser);
            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = DIRECTOR_ID
            });
        

    }

        public DbSet<StudentFromWebsite> studentsFromWebsite { get; set; }
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
