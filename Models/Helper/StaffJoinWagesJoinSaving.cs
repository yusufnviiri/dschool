namespace victors.Models.Helper
{
    public class StaffJoinWagesJoinSaving
    {
        public Staff Staff { get; set; } = new();
        public List<Wage> Wages { get; set; } = new();
        public List<Saving> Savings { get; set; } = new();

        public IEnumerable<Wage>? LastThreeWages { get; set; } = new List<Wage>();
        public IEnumerable<Saving>? LastFiveSavings { get; set; } = new List<Saving>();
    }
}
