using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Models
{
    public class OrderModel
    {
        [Required]
        public string Destination { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }


    }
}
