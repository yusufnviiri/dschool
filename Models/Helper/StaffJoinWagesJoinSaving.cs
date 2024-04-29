namespace victors.Models.Helper
{
    public class StaffJoinWagesJoinSaving
    {
        public Staff Staff { get; set; } = new();
        public List<Wage> Wages { get; set; } = new();
        public List<Saving> Savings { get; set; } = new();
    }
}
