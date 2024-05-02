namespace victors.Models
{
    public class ExamCache
    {
        public int ExamCacheId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Boolean ExamSession { get; set; } = false;
    }
}
