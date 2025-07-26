namespace victors.Models.Helper
{
    public class StudentIndex
    {
        public ICollection<Student> Students { get; set; }=new List<Student>();
        public LookUpStudents LookUpStudents { get; set; } = new LookUpStudents();
        public List<Student> PagedStudents { get; set; } = new();
        public List<string> AllGrades { get; set; } = new();

        public string SearchTerm { get; set; }
        public string GradeFilter { get; set; }

        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
