using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class PaceScores
    {
        public int PaceScoresId { get; set; }
        public int courseId { get; set; }
        public int StudentId { get; set;}
        [Required(ErrorMessage = "Pace Score  is required")]

        public int paceScore { get; set; }
        public string term { get; set; } = string.Empty;
    }
}
