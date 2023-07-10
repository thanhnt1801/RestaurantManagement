using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; } = false;
        public virtual IList<Price>? Prices { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public int? RestaurantId { get; set; }
        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public virtual IList<ItemImage>? ItemImages { get; set; }
    }
}
