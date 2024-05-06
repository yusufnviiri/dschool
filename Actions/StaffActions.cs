using victors.Models;
using victors.Models.Context;
using victors.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace victors.Actions
{
    public class StaffActions
    {
        public ICollection<Staff> staffs {  get; set; }= new List<Staff>();
        public ICollection<Wage> wages { get; set; } = new List<Wage>();
        public ICollection<Saving> savings { get; set; } = new List<Saving>();
        public Staff staff { get; set; } =new Staff();
        public async Task<Staff> CreateStaff(ApplicationDbContext _db,Staff staff)
        {
            if (staff == null)
            {
                return new Staff();
            } else
            {
                staff.AgeCalc(staff.Birthdate);
                await _db.staffs.AddAsync(staff);
                await _db.SaveChangesAsync();
                return staff;
            }
        }

        public async Task<ICollection<Staff>> GetAllStaff(ApplicationDbContext _db)
        {
            staffs = await _db.staffs.ToListAsync();
            return staffs;
        }

        public async Task<ICollection<Staff>> SearchStaff(ApplicationDbContext _db,LookUpStaff data)
        {
            var Fname = data.FName;var Lname = data.LName; var Sstatus = data.Sstatus;var scategory = data.SCategory; var sjob = data.SJobTitle;
            if (Fname != null)
            {
                staffs= await _db.staffs.Where(k=>k.FirstName.ToLower().Contains(Fname.ToLower())).ToListAsync();
            }
            else
            {
                staffs = await _db.staffs.ToListAsync();
            }


            if (Lname != null)
            {
                staffs = staffs.Where(k => k.LastName.ToLower().Contains(Lname.ToLower())).ToList();
            }
            
            if (Sstatus != null)
            {
                staffs = staffs.Where(k => k.Status.ToLower().Contains(Sstatus.ToLower())).ToList();
            }
            if (scategory != null)
            {
                staffs = staffs.Where(k => k.Category.ToLower().Contains(scategory.ToLower())).ToList();
            }
            if (sjob != null)
            {
                staffs = staffs.Where(k => k.JobTitle.ToLower().Contains(sjob.ToLower())).ToList();
            }



            return staffs;
        }

        public async Task<StaffJoinWagesJoinSaving> GetStaff(ApplicationDbContext _db,int Id)
        {
            staff = await _db.staffs.FindAsync(Id);
            wages= await _db.wages.ToListAsync();
            savings = await _db.Savings.ToListAsync();
            var data = new StaffJoinWagesJoinSaving
            {
                Staff = staff,
                Wages = wages.Where(k => k.StaffId == Id).ToList(),
                Savings = savings.Where(k => k.StaffId == Id).ToList(),
                LastFiveSavings = savings.Where(k => k.StaffId == Id).ToList().Take(5),
                LastThreeWages = wages.Where(k => k.StaffId == Id).ToList().Take(3),
            };
            return data;
        }
        public async Task<Wage> payStaffWages(ApplicationDbContext _db, Wage wage)
        {
            staff = await _db.staffs.FindAsync(wage.StaffId);
            var saving = new Saving();
            if (wage == null)
            {
                return new Wage();
            }
            else
            {  wage.Staff=staff;
                wage.BalanceCalculator(staff,wage.Amount,wage.Balance);
                await _db.wages.AddAsync(wage);                
                await _db.SaveChangesAsync();
                saving.StaffId= wage.StaffId;
                saving.savingAmount = wage.Compulserysavings;
                await _db.Savings.AddAsync(saving);
                await _db.SaveChangesAsync();
                return wage;
            }
        }


        public async Task<Staff> UpdateStaff(ApplicationDbContext _db, Staff staff)
        { var oldStaff = await _db.staffs.FindAsync(staff.StaffId);
            oldStaff.Birthdate=staff.Birthdate;
            oldStaff.FirstName=staff.FirstName;
            oldStaff.LastName=staff.LastName;
            oldStaff.Salary=staff.Salary;
            oldStaff.Category=staff.Category;
            oldStaff.Contact=staff.Contact; 
            oldStaff.Status=staff.Status;
            oldStaff.Email=staff.Email;
            oldStaff.Gender=staff.Gender;
            oldStaff.District=staff.District;
            oldStaff.JobTitle=staff.JobTitle;
            oldStaff.Qualification=staff.Qualification;
            oldStaff.Village=staff.Village;
            await _db.SaveChangesAsync();
            return oldStaff;
        }
                public async Task<ICollection<Wage>> GetAllWages(ApplicationDbContext _db)
        {
            var wagesGroup = await _db.wages.Include(k=>k.Staff).GroupBy(h => h.PayMonth).ToListAsync();
            staffs = await _db.staffs.ToListAsync();
            var staffWages = (from pay in wagesGroup select new Wage
            {

            }).ToList();
            //var monthlyWages  = staffWages.GroupBy(k=>k.PayMonth).ToList();
                    
            return staffWages;
        } 
        }
    }
