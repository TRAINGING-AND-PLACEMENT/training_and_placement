using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "You must provide a email address!")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}