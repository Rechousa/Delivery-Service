using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Database
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public int LocationA { get; set; }

        [Required]
        public int LocationB { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public int Cost { get; set; }
    }
}
