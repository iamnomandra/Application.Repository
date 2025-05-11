
namespace Chat.Application.Entities
{ 
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Product { get; set; }
        public long? Rate { get; set; }
        public long? Quantity { get; set; }
        public string? ImagePath { get; set; }
    }
}
