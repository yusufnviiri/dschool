namespace victors.Models
{
    public class Saving
    {

        public int SavingId { get; set; }
        public int StaffId { get; set; }
        public decimal savingAmount { get; set; }
        public string savingDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";

    }
}
