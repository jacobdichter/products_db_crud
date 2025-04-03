namespace ProductsAppRP.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockLevel { get; set; }
        public string? Description { get; set; }
        public int? Pcolor { get; set; }
        public bool OnSale { get; set; }
        public bool Discontinued { get; set; }
    }
}