using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deliveroo.Models
{
    public class OrderModel
    {

        [Required]
        [Range(1, 10)]
        public int NumberOfBaggages { get; set; }

        [Required]
        [Range(5, 200)]
        public int TotalWeight { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Guid AddressID { get; set; }




    }
}
