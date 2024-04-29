namespace victors.Models
{
    public class Staff:Person
    {
        public int StaffId{ get; set; }
        public string Category { get; set;}
        public decimal Salary { get; set;}
        public string JobTitle { get; set; }
        public string MaritalStatus { get; set; }
        public string Qualification { get; set; }

    }
}
