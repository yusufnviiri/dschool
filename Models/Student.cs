namespace victors.Models
{
    public class Student:Person
    {
        public int StudentId { get;set; }
        public string Section{ get; set; } = string.Empty;
        public string Grade { get; set; }=string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
        public string Term { get; set; } = string.Empty;
        public string Stream { get; set; } = string.Empty;
        public decimal SchoolFees { get; set; } = 0M;
        public  string birthDateString { get; set; } = string.Empty;
        public decimal charge { get; set; } = 0M;
        public bool fullUniform { get; set; } = false;

        public void setDateString(DateTime date)
        {
            birthDateString = $"{date.Day}/{date.Month}/{date.Year}";
        }

    }
    
}
