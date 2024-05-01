namespace victors.Models.Helper
{
    public class StudentJoinGuardian
    {
        public Guardian Guardian { get; set; }=new Guardian();
        public Student Student { get; set; }=new Student();
    }
}
