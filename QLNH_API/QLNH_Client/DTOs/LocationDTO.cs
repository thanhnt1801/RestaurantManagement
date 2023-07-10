using QLNH_Client.DTOs;
using QLNH_Client.Models;

namespace QLNH_Client.DTOs
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int? RestaurantId { get; set; }
        public List<GuestTableDTO>? GuestTables { get; set; }
        public bool Deleted { get; set; } = false;

    }
}
