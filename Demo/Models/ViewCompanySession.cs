
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
        
        [AllowNull]
        [ValidateNever]
        public Hiring_Departments Hiring_Departments { get; set; }

        [AllowNull]
        [ValidateNever]
        public  Hiring_sectors Hiring_sectors { get; set; }

        public Hiring Hiring { get; set; }  

        //public long company_id { get; set; }
        
        //public string designation { get; set; }
        
        //public string other_requirement { get; set; }

        //public string department_name { get; set; }
        
        //public string sector_name { get; set; }

        //public string fix_salary { get; set; }


    }
}
