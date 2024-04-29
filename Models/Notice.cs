using Microsoft.VisualBasic;

namespace victors.Models
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public DateTime DueDate { get; set; }= DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set;} = string.Empty;
        public string Postedby { get; set;}= string.Empty;
        public string dueDateString { get; set; } = string.Empty;
        public string NoticeDate { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";

        public void SetDate(DateTime date)
        {
            dueDateString= $"{date.Day}/{date.Month}/{date.Year}";
        }
    }
}
    
