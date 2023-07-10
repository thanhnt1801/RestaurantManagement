using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QLNH_Client.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public bool OffDuty { get; set; } = true;
        public List<Restaurant>? RestaurantCreator { get; set; }
        public List<Restaurant>? RestaurantUpdator { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual IList<Guest> GuestCreator { get; set; }
        public virtual IList<Guest> GuestUpdator { get; set; }

    }
}
