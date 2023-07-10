namespace QLNH_Client.DTOs
{
    public class GuestTableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RestaurantId { get; set; }
        public int? LocationId { get; set; }
        public LocationDTO? Location { get; set; }
        public int? StatusId { get; set; }
        public StatusDTO? Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}
