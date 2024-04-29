namespace victors.Models.Helper
{
    public class AssessementSumary
    {
        public Student student { get; set; }
        public Course course { get; set; }
        public List<Assessement> assessements { get; set; }
        public List<double> Pacescores { get; set; } = new List<double>();
        public List<PaceScores> summary { get; set; }= new List<PaceScores>();
        public double Final { get; set;} = 0;

    }
}
