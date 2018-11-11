using DeliveryService.Database;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common
{
    public class LocationEditVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public static Location ToLocation(LocationEditVM location)
        {
            var result = new Location
            {
                Id = location.Id,
                Name = location.Name
            };
            return result;
        }
        public static Location UpdateLocationData(Location location, LocationEditVM locationVM)
        {
            location.Id = locationVM.Id;
            location.Name = locationVM.Name;
            return location;
        }
    }
}
