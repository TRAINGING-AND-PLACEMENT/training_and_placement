using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class DepartmentSectorCsv
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.csv)$", ErrorMessage = "Only csv Files Allowed")]
        public IFormFile CSV_File { get; set; }
    }
}
