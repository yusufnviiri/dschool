using Microsoft.EntityFrameworkCore.Query;

namespace victors.Models
{
    public class CashFlow
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AmountSpent { get; set; }
        public decimal AmountDue { get; set; }
        public int StudentList { get; set; } = 0;
        public int StaffList { get; set; } = 0;
        public int Guardianlist { get; set; } = 0;
        public int PettyCashList { get; set; } = 0;
        public int AssetsCount { get; set; } = 0;
        public int NoticesList { get; set; } = 0;
     






    }
}
