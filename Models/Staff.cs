using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Staff:Person
    {
        public int StaffId{ get; set; }
        [Required(ErrorMessage = "Category is required")]

        public string Category { get; set;}
        [Required(ErrorMessage = "Enter Staff Salary")]

        public decimal Salary { get; set;}
        [Required(ErrorMessage = " Job Title is required")]

        public string JobTitle { get; set; }
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Qualification  is required")]

        public string Qualification { get; set; }

    }
}
