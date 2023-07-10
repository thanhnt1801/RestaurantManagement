using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; } = false;
        public virtual User? CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public int? UpdatedById { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public int? RestaurantId { get; set; }

    }
}
