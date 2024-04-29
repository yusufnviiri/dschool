namespace victors.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty ;
        public string Grade { get; set; } = string.Empty;
        public string Stream { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;

        public void stringToUpperCase(string str)
        {
            Name = str.ToUpper();
        }


    }
}
