namespace victors.Models.Helper
{
    public class StudentIndex
    {
        public ICollection<Student> Students { get; set; }=new List<Student>();
        public LookUpStudents LookUpStudents { get; set; } = new LookUpStudents();
    }
}
