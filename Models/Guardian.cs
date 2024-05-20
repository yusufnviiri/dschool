using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Guardian:Person
    {
        public int GuardianId { get; set; }
        [Required(ErrorMessage = " Relationship is required")]

        public string Relationship { get; set; }=string.Empty;
        public int StudentId { get; set; }
        [Required(ErrorMessage = " Place of Origin is required")]

        public string PlaceofOrigin { get; set; } =string.Empty;
    }
}
