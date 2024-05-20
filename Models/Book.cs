using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = " Title  is required")]

        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author  is required")]

        public string Author { get; set; } = string.Empty;
        [Required(ErrorMessage = "Category  is required")]

        public string Category { get; set; } = string.Empty;


    }
}
