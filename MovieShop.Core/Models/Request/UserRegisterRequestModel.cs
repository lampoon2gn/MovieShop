using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        [EmailAddress]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(maximumLength:50,ErrorMessage ="Make sure your password is between 8-50 characters",MinimumLength =8)]
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
    }
}
