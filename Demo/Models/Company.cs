using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Company
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Company Identification Number")]
        [Required(ErrorMessage = "You must provide a identification number!")]
        public string cif { get; set; }

        [Required(ErrorMessage = "You must provide a name!")]
        public string name { get; set; }

        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        public string contact { get; set; }

        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string alt_contact { get; set; }

        [Required(ErrorMessage = "You must provide a emailaddress!")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "You must provide a emailaddress!")]
        public string alt_email { get; set; }

        public int status { get; set; } = 0;

        [AllowNull]
        public string remarks { get; set; }
    }
}
