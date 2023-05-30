
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class ViewCompnaySession
    {
        [AllowNull]
        [ValidateNever]
        public IEnumerable<Company> Companies { get; set; }


        [AllowNull]
        [ValidateNever]
        public IEnumerable<Department> Departments { get; set; }

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

    }
}
