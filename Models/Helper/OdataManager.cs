namespace victors.Models.Helper
{
    public class OdataManager
    {
        public Student student { get; set; } = new();
        public SchoolFees schoolFees { get; set; } = new();
        public List<Student> students = new();
        public User User { get; set; } = new();
        public int UserId { get; set; }
        public string ResponseData { get; set; }
        public bool RequestConfirmation { get; set; } = false;
        public IEnumerable<User> Users { get; set; }
    }
}
