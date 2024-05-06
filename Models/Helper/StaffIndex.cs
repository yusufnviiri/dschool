namespace victors.Models.Helper
{
    public class StaffIndex
    {
        public LookUpStaff LookUpStaff { get; set; }= new LookUpStaff();
        public ICollection<Staff> Staffs { get;set; }= new List<Staff>();
    }
}
