using System.ComponentModel.DataAnnotations;

namespace victors.Models
{
    public class Asset
    {
        public int Assetid { get; set; }
        [Required(ErrorMessage = "Name  is required")]

        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = " Description is required")]

        public string Description { get; set; }= string.Empty;
        [Required(ErrorMessage = "Supplier  is required")]

        public string Supplier { get; set; } = string.Empty;
        public decimal AssetValue { get; set; } = decimal.Zero;
        [Required(ErrorMessage = "Supplier Contact  is required")]

        public string  SupplierContact {  get; set; } = string.Empty;
        public string Date { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
    }
}
