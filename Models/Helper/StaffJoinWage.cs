namespace victors.Models.Helper
{
    public class StaffJoinWage
    {
        public Staff Staff { get; set; } = new Staff();
        public List<Wage> Wages { get; set; } = new List<Wage>();
    }
}
