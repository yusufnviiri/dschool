using System.Reflection.Metadata;

namespace victors.Models
{
    public class Person
    {
       
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Village { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int Age { get; set; }
        public String Religion { get; set; }= string.Empty;
        public DateTime Birthdate { get; set; } = DateTime.Now.Date;
        public string Status { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";


        public void AgeCalc(DateTime bithdate)
        {
            Age = DateTime.Today.Year - bithdate.Year;
        }

    }
}
