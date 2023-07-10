namespace QLNH_Client.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? RestaurantId { get; set; }

    }
}
