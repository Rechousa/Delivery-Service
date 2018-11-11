using DeliveryService.Database;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common
{
    public class LocationAddVM
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public static Location ToLocation(LocationAddVM location)
        {
            var result = new Location
            {
                Name = location.Name
            };
            return result;
        }
    }
}
