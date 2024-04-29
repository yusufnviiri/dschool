namespace victors.Models
{
    public class PettyCash
    {
        public int PettyCashId { get; set; }
        public string dateString { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        public string  Category { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; } = decimal.Zero;
        public string PaidBy { get; set; } = string.Empty ;
        public string RecievedBy { get; set; } = string.Empty;
       

    }
    
}
