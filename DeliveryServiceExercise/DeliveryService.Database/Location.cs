using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Database
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
