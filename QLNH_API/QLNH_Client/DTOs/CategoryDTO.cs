using QLNH_Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNH_Client.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? ParentId { get; set; }
        public virtual CategoryDTO? Parent { get; set; }
        public virtual ICollection<CategoryDTO>? Children { get; set; }
        public virtual RestaurantDTO? Restaurant { get; set; }
        public int? RestaurantId { get; set; }
    }
}
