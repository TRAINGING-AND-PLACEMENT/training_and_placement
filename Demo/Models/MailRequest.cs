using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class MailRequest
    {
        [Required(ErrorMessage = "You must provide a email address!")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string ToEmail { get; set; }

        [ValidateNever]
        public string Subject { get; set; }

        [ValidateNever]
        public string Body { get; set; }
    }
}
