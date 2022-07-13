using System;

namespace Deliveroo.Data.Entities
{
    public class Address
    {
        public Guid AddressID { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string StreetName { get; set; }


    }
}
