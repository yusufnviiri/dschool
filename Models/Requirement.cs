namespace victors.Models
{
    public class Requirement:Payment
    {
        public int RequirementId { get; set; }
        public string Name { get; set; }= string.Empty;
        public decimal Charge { get; set; } = 0M;
     

    }
}
