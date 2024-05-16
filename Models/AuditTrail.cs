namespace victors.Models
{
    public class AuditTrail
    {


        public int AuditTrailId { get; set; }
        public string? Reason { get; set; } = "Login";
        public string LogInDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
    }
}
