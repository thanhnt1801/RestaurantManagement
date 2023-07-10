using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public int? RestaurantId { get; set; }

        public int? ItemId { get; set; }

        public int? SizeId { get; set; }

        public int? UnitId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant? Restaurant { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item? Item { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size? Size { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit? Unit { get; set; }
        public string Description { get; set; }
        public double SalePrice { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
