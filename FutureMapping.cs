namespace MyWebAPI.Models
{
    public class FutureMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contract { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Price { get; set; }
    }
}
