namespace victors.Models.Helper
{
    public class NoticeJoinCashFlow
    {
        public ICollection<CashFlow> CashFlows { get; set; } = new List<CashFlow>();
        public ICollection<Notice> Notices { get; set; } = new List<Notice>();
    }
}
