namespace victors.Models
{
    public class Academics
    {


            public int StudentId { get; set; }
            public string Grade { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
        public string Term { get; set; } = string.Empty;
            public string Stream { get; set; } = string.Empty;
            public decimal SchoolFees { get; set; } = 0M;
            public string Section { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
        public string contact {  get; set; } = string.Empty;
            public string RegistrationDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";



        
    }

}
