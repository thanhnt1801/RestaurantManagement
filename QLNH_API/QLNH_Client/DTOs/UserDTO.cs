using QLNH_Client.Models;

namespace QLNH_Client.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public bool OffDuty { get; set; } = true;
        public int? RoleId { get; set; }
        public RoleDTO? Role { get; set; }
        public RestaurantDTO? Restaurant { get; set; }

    }
}
