using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace victors.Models
{
    public class Person
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }= string.Empty;
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "village is required")]

        public string Village { get; set; } = string.Empty;
        [Required(ErrorMessage = "District is required")]

        public string District { get; set; } = string.Empty;
        [Required(ErrorMessage = "Student contact is missing")]

        public string Contact { get; set; } = string.Empty;
        [Required(ErrorMessage = "Missing Nationality")]

        public string Nationality { get; set; } = string.Empty;
        public int Age { get; set; }
        public String Religion { get; set; }= string.Empty;
        public DateTime Birthdate { get; set; } = DateTime.Now.Date;
        [Required(ErrorMessage = "Status missing")]

        public string Status { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";


        public void AgeCalc(DateTime bithdate)
        {
            Age = DateTime.Today.Year - bithdate.Year;
        }

    }
}
