using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class GuestTable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public virtual Restaurant? Restaurant { get; set; }
        public int? RestaurantId { get; set; }

        public virtual Location? Location { get; set; }
        public int? LocationId { get; set; }

        public virtual Status? Status { get; set; }
        public int? StatusId { get; set; }
    }
}
