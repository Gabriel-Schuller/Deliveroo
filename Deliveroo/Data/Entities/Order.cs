using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Data.Entities
{
    public class Order
    {
        public Guid OrderID { get; set; }

        public Guid UserID { get; set; }

        public User User { get; set; }

        public List<Package> Package { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        public Address address { get; set; }



    }
}
