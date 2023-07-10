using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace QLNH_Client.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public int? CreatedUserId { get; set; }
        public int? UpdatedUserId { get; set; }
        public User CreatedUser { get; set; }
        public User UpdatedUser { get; set; }
        public virtual IList<User> Users { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<Item> Items { get; set; }
        public virtual IList<ItemImage> ItemImages { get; set; }
        public virtual IList<Price> Prices { get; set; }
        public virtual IList<Size> Sizes { get; set; }
        public List<Guest> Guests { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Location> Locations { get; set; }
        public List<GuestTable> GuestTables { get; set; }
        public List<Unit> Units { get; set; }
    }
}
