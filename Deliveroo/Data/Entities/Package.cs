using System;
using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Data.Entities
{
    public class Package
    {
        public Guid PackageID { get; set; }

        [Required]
        public string Weight { get; set; }

    }
}
