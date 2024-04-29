namespace victors.Models.Helper
{
    public class ReportCourse
    {
        public Course course { get; set; } = new Course();
        public double score { get; set; } =0;
        public List<Assessement> ? assessements { get; set; }= new List<Assessement> ();
    }
}
