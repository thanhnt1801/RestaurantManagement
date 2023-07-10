using QLNH_Client.Models;

namespace QLNH_Client.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public UserDTO? CreatedUser { get; set; }
        public UserDTO? UpdatedUser { get; set; }
        public IList<UserDTO>? Users { get; set; }

    }
}
