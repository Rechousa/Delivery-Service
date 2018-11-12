using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common
{
    public class UserAuthenticationVM
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}