using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deliveroo.Data.Entities
{
    public class Order
    {
        public Guid OrderID { get; set; }

        public Guid UserID { get; set; }

        public User User { get; set; }

        [Required]
        [Range(1,10)]
        public int NumberOfBaggages { get; set; }
        [Required]
        [Range(5,200)]
        public int TotalWeight { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Address Address { get; set; }

        public int AproxCost { get; set; }



    }
}
