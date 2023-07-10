using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; } = false;
        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public List<GuestTable> GuestTables { get; set; }
    }
}
