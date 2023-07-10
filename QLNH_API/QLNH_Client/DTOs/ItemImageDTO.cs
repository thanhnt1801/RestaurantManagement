using QLNH_Client.Models;
using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.DTOs
{
    public class ItemImageDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Base64 { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? RestaurantId { get; set; }
        public int? ItemId { get; set; }
    }
}
