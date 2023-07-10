using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; } = false;
        public List<User> User { get; set; }
    }
}
