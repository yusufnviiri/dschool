namespace victors.Models.Helper
{
    public class AssessementJoinCourseJoinStudent
    {

        public Student student { get; set; }= new ();
        public Assessement assessement { get; set; } = new();
        public Course course { get; set; } = new(); 
    }
}
