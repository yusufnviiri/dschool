namespace victors.Models.Helper
{
    public class ReportCourse
    {
        public Course course { get; set; } = new Course();
        public double score { get; set; } =0;
        public Student? student { get; set; }= new Student();
        public List<Assessement> ? assessements { get; set; }= new List<Assessement> ();
    }
}
