
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

        [AllowNull]
        [ValidateNever]
        public IEnumerable<Sector> Sectors { get; set; }
        
        public Hiring Hiring { get; set; }  

    }
}
