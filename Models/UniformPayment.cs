namespace victors.Models
{
    public class UniformPayment
    {
        public int UniformPaymentId { get; set; }
        public int UniformId { get; set; }
        public decimal Amount { get; set; } = 0M;
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string UniformDetails { get; set; } = string.Empty;

        public string RecievedBy { get; set; } = string.Empty;
        public string DateOfPayment { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";

        public void SetValueAttributes(Uniform uniform)
        {
            UniformDetails = uniform.Item+ " " + " for " + "  " + uniform.AcademicLevel;
        }

    }
}
