using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deliveroo.Models
{
    public class OrderModel
    {
        [Required]
        public string Destination { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;


    }
}
