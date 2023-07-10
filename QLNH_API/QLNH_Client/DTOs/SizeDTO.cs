using QLNH_Client.Models;

namespace QLNH_Client.DTOs
{
    public class SizeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public int? RestaurantId { get; set; }
        public virtual IList<PriceDTO>? Prices { get; set; }
    }
}
