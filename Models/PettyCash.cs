using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class PettyCash
    {
        public int PettyCashId { get; set; }
        public string dateString { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        [Required(ErrorMessage = " Category is required")]

        public string  Category { get; set; }= string.Empty;
        [Required(ErrorMessage = "Description  is required")]

        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = " Enter Amount")]

        public decimal Amount { get; set; } = decimal.Zero;
        [Required(ErrorMessage = " This field is required")]

        public string PaidBy { get; set; } = string.Empty ;
        public string RecievedBy { get; set; } = string.Empty;
       

    }
    
}
