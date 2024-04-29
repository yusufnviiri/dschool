namespace victors.Models.Helper
{
    public class FeesJoinStudent
    {
        public SchoolFees SchoolFees { get; set; } = new SchoolFees();
        public Student Student { get; set; } = new Student();
    }
}
