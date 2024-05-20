using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Requirement:Payment
    {
        public int RequirementId { get; set; }
        [Required(ErrorMessage = "Item Description required")]

        public string Name { get; set; }= string.Empty;
        [Required(ErrorMessage = "Amount required")]

        public decimal Charge { get; set; } = 0M;
     

    }
}
