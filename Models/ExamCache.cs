namespace victors.Models
{
    public class ExamCache
    {
        public int ExamCacheId { get; set; }
        public Course? Course { get; set; }=new Course();
        public Student? Student { get; set; }=new Student();
    }
}
