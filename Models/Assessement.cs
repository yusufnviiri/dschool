namespace victors.Models
{
    public class Assessement
    {
        public int AssessementId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string Term { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Stream { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string PaceNumber { get; set; } = string.Empty;
        public int PaceScore { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;

        public void stringToUpperCase(string str)
        {
            PaceNumber = str.ToUpper();
        }

    }
}
