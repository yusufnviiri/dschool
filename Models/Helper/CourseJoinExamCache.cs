namespace victors.Models.Helper
{
    public class CourseJoinExamCache
    {
        public ICollection<Course> courses { get; set; }=new List<Course>();    
        public ExamCache examCache { get; set; } = new ExamCache();
    }
}
