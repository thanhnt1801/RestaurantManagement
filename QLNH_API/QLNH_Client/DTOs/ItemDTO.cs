
namespace QLNH_Client.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? RestaurantId { get; set; }
        public int? CategoryId { get; set; }
        public IList<PriceDTO>? Prices { get; set; }
    }
}
