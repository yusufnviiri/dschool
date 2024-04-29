namespace victors.Models
{
    public class RequirementPayment:Payment
    {
        public int RequirementPaymentId { get; set; }
        public int RequirementId { get; set; }
        public string RequiremntName { get; set; } = string.Empty;
    }
}
