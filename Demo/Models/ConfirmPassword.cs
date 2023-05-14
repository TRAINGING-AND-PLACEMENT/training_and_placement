
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class ConfirmPassword
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide otp")]
        [MaxLength(5, ErrorMessage = "Length of otp must be of 5!!")]
        [MinLength(5, ErrorMessage = "Length of otp must be of 5!!")]
        [DisplayName("Enter otp")]
        public string otp { get; set; }

        [Required(ErrorMessage = "Enter new password")]
        [DisplayName("Enter new password")]
        [Compare("password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Password doesn't match")]
        [DisplayName("Confirm new password")]
        public string cpassword { get; set; }
    }
}
