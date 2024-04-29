namespace victors.Models
{
    public class Uniform
    {
        public int UniformId { get; set; } 
        public string Item { get; set; } = string.Empty;
        public string AcademicLevel { get; set; } = string.Empty;
        public decimal Cost { get; set; } = decimal.Zero;


    }
}
