namespace victors.Models.Helper
{
    public class StudentJoinRequirement
    {
        public Student Student { get; set; }= new Student();
        public Requirement Requirement { get; set; }  = new Requirement();
        public List<Requirement>? Requirements { get; set; }=new List<Requirement>();
    }
}
