using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Models
{
    public class UserModel
    {

        public string UserName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserPhoneNumber { get; set; }

    }
}
