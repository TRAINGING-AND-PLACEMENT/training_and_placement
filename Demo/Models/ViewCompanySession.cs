
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class ViewCompanySession
    {
        [AllowNull]
        [ValidateNever]
        public IEnumerable<Company> Companies { get; set; }


        [AllowNull]
        [ValidateNever]
        public IEnumerable<Department> Departments { get; set; }

        [AllowNull]
        [ValidateNever]
        public IEnumerable<Sessions> Sessions { get; set; }
        [ValidateNever]
        [AllowNull]
        public Department department { get; set; }

        [AllowNull]
        [ValidateNever]
        public IEnumerable<Sector> Sectors { get; set; }
        [AllowNull]
        [ValidateNever]
        public IEnumerable<Hiring_Departments> Hiring_Departments { get; set; }
        [AllowNull]
        [ValidateNever]
        public IEnumerable<Hiring_sectors> Hiring_sectors { get; set; }

        public Hiring Hiring { get; set; }


        [DisplayName("Session Name")]
        [Required(ErrorMessage = "You must provide a Session Name!")]
        public List<string> multisession { get; set; }

    }
}
