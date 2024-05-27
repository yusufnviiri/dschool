using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class StudentFromWebsite:Person
    {
        public int StudentFromWebsiteId { get; set; }
        public Boolean Approved { get; set; } = false;
        public string Section { get; set; } = string.Empty;
        [Required(ErrorMessage = "Grade is required")]

        public string Grade { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
        [Required(ErrorMessage = "Term is required")]

        public string Term { get; set; } = string.Empty;

        public string Stream { get; set; } = string.Empty;
        [Required(ErrorMessage = "School Fees is required")]

        public decimal SchoolFees { get; set; } = 0M;
        public string birthDateString { get; set; } = string.Empty;
        public decimal charge { get; set; } = 0M;
        public bool fullUniform { get; set; } = false;

        public void setDateString(DateTime date)
        {
            birthDateString = $"{date.Day}/{date.Month}/{date.Year}";
        }




    }
}
