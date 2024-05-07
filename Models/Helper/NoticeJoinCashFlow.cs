namespace victors.Models.Helper
{
    public class NoticeJoinCashFlow
    {
        public IEnumerable<CashFlow> CashFlows { get; set; } = new List<CashFlow>();
        public Array CashFlowArray { get; set; }
        public ICollection<Notice> Notices { get; set; } = new List<Notice>();
    }
}
