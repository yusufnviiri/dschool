namespace victors.Models
{
    public class Asset
    {
        public int Assetid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public string Supplier { get; set; } = string.Empty;
        public decimal AssetValue { get; set; } = decimal.Zero;
        public string  SupplierContact {  get; set; } = string.Empty;
        public string Date { get; set; } = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
    }
}
