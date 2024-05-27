﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using victors.Actions;
using victors.Models;
using victors.Models.Context;
using victors.Models.Helper;

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
            return RedirectToAction("PendingApplications");
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
            Student newEntry = new Student();
            await studentActions.createSudents(newEntry,_db);
            await studentActions.RejectWebStudent(1, _db);
            return RedirectToAction("PendingApplications");
        }
    }
}
