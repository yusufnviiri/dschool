using victors.Models;
using victors.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace victors.Actions
{
    public class Affairs
    {

        public ICollection<PettyCash> PettyCashes { get; set; } = new List<PettyCash>();
        public ICollection<UniformPayment> uniformPayments { get; set; }= new List<UniformPayment>();
        public ICollection<Student>studentList { get; set; } = new List<Student>();
        public ICollection<Book> books { get; set; } = new List<Book>();
        public Book Book { get; set; } = new();

        public ICollection<Staff> staffList { get; set; } = new List<Staff>();
        public ICollection<Guardian> guardianlist { get; set; } = new List<Guardian>(); 
        public ICollection<Asset> Assets { get; set; }= new List<Asset>();
        public ICollection<Notice> Notices { get; set; }=new List<Notice>();
        public ICollection<CashFlow> CashFlowsList { get; set; } = new List<CashFlow>();
        public PettyCash pettyCash { get; set; } = new PettyCash();

        public async Task<PettyCash> AddPettyCash(ApplicationDbContext _db,PettyCash transaction)
        {
            await _db.pettyCashes.AddAsync(transaction);            
            await _db.SaveChangesAsync();
            return transaction;
        }

        public async Task<ICollection<PettyCash>> GetPettyCash( ApplicationDbContext _db)
        {
            PettyCashes = await _db.pettyCashes.ToListAsync();
            return PettyCashes;
        }

        public async Task<Asset> postAsset (ApplicationDbContext _db,Asset asset)
        {
            await _db.assets.AddAsync(asset);
            await _db.SaveChangesAsync();
            return asset;
        }
        public async Task<ICollection<Asset>> getAllAssets(ApplicationDbContext _db) 
        {
       Assets = await _db.assets.ToListAsync();
            return Assets;
        }
        public async Task<Asset>FindAsset(ApplicationDbContext _db,int id)
        {
           var asset = await _db.assets.FindAsync(id);
            return asset;
        }
        public async Task<Notice> postNotice(ApplicationDbContext _db, Notice notice)
        {
            await _db.Notices.AddAsync(notice);
            await _db.SaveChangesAsync();
            return notice;
        }
        public async Task<ICollection<Notice>> getAllNotice(ApplicationDbContext _db)
        {
           Notices = await _db.Notices.ToListAsync();
            return Notices;
        }

        public async Task<ICollection<Notice>> GetWebFeedBackNotices(ApplicationDbContext _db)
        {
            string nameTag = "feedback";
            Notices = await _db.Notices.Where(p=>p.Postedby.ToLower().Contains(nameTag)).ToListAsync();
            return Notices;
        }
        public async Task<Notice> FindNotice(ApplicationDbContext _db, int id)
        {
            var notice = await _db.Notices.FindAsync(id);
            return notice;
        }


        public async Task<Book> CreateBook(ApplicationDbContext _db, Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return book;
            ;
        }
        public async Task<ICollection<Book>> GetallBooks(ApplicationDbContext _db)
        {
            books = await _db.Books.ToListAsync();
            return books;
        }

            public async Task<ICollection<CashFlow>> CashFlows(ApplicationDbContext _db)
        {
          studentList  = await _db.Students.ToListAsync();
            staffList = await _db.staffs.ToListAsync();
            guardianlist = await _db.guardians.ToListAsync();
            PettyCashes = await _db.pettyCashes.ToListAsync();
            Assets = await _db.assets.ToListAsync();
            Notices = await _db.Notices.ToListAsync();
            uniformPayments = await _db.UniformPayments.ToListAsync();
            CashFlow wages = new ()
            {
                Type = "Expense",
                Description = "Wages",
                AmountDue = _db.wages.Sum(k => k.Balance),
                AmountSpent = _db.wages.Sum(k => k.Amount)
            };
            CashFlowsList.Add(wages);
            CashFlow uniformpayList = new ()
            {
                Type="Income",
                Description="Uniform Payments",
                AmountDue = 0,
                AmountSpent = uniformPayments.Sum(k=>k.Amount),

            };
            CashFlow requirements = new ()
            {
                Type = "Income",
                Description = "School Requirements",
                AmountDue = _db.RequirementsPayment.Where(k=>k.StudentId !=0).Sum(k => k.Balance),
                AmountSpent = _db.RequirementsPayment.Where(k => k.StudentId != 0).Sum(k => k.Amount),
            };
            CashFlowsList.Add(requirements);
            CashFlow dailyExpenses = new()
            {
                Type = "Expense",
                Description = "Daily Expenses",
                AmountDue = 0,
                AmountSpent = _db.pettyCashes.Sum(k => k.Amount),
            };
            CashFlowsList.Add(dailyExpenses);
            CashFlow schoolFees = new()
            {
                Type = "Income",
                Description = "School Fees",
                AmountDue = _db.schoolFees.Sum(k => k.Balance),
                AmountSpent = _db.schoolFees.Sum(k => k.Amount)
            };
            CashFlowsList.Add(schoolFees);
            CashFlow EntityCount = new()
            {
                AssetsCount = Assets.Count(),
                NoticesList = Notices.Count(),
                StaffList = staffList.Count(),
                StudentList = studentList.Count(),
                Guardianlist = guardianlist.Count(),
                PettyCashList = PettyCashes.Count()
            };
            CashFlowsList.Add(EntityCount);
            CashFlowsList.Add(uniformpayList);


            return CashFlowsList;

        }
    }
}
