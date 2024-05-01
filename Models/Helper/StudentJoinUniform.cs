namespace victors.Models.Helper
{
    public class StudentJoinUniform
    {
        public UniformPayment? uniformPayment { get; set; } = new();
        public Uniform? uniform { get; set; } = new();
        public ICollection<Uniform>? uniforms { get; set; }=new List<Uniform>();
        public Student? student { get; set; } = new ();
    }
}
