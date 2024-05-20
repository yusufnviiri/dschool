using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Payment
    {
        [Required(ErrorMessage = " Amount is required")]
        public decimal Amount { get; set; } 
        public string DateOfPayment { get; set; }= $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        public string RecievedBy { get; set; } = string.Empty;
        public string PaidBy { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal AmountDue { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string Reason { get; set; } = "school fees payment";

        public string Term { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
        public int StudentId { get; set; }
        public string DateFormat = string.Empty;
    }
}
