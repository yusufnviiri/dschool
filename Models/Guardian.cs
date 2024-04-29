namespace victors.Models
{
    public class Guardian:Person
    {
        public int GuardianId { get; set; }
        public string Relationship { get; set; }=string.Empty;
        public int StudentId { get; set; }
        public string PlaceofOrigin { get; set; } =string.Empty;
    }
}
