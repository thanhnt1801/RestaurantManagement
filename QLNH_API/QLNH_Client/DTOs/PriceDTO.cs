namespace QLNH_Client.DTOs
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int SizeId { get; set; }
        public SizeDTO? Size { get; set; }
        public int UnitId { get; set; }
        public UnitDTO? Unit { get; set; }
        public int? RestaurantId { get; set; }
        public string Description { get; set; }
        public double SalePrice { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;

    }
}
