namespace victors.Models.Helper
{
    public class StudentJoinUniform
    {
        public UniformPayment uniformPayment { get; set; } = new();
        public Uniform uniform { get; set; } = new();
        public Student student { get; set; } = new ();
    }
}
