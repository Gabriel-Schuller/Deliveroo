using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Data.Entities
{
    public class User
    {
        public Guid UserID { get; set; }

        public string UserName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Order> Order { get; set; }

        public string UserPhoneNumber { get; set; }


    }
}
