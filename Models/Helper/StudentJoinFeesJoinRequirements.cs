namespace victors.Models.Helper
{
    public class StudentJoinFeesJoinRequirements
    {
        public Student student { get; set; }= new Student();
        public IEnumerable<SchoolFees> schoolFees { get; set; }=new List<SchoolFees>();
        public IEnumerable<RequirementPayment> requirementPayments { get; set; }= new List<RequirementPayment>();
    }
}
