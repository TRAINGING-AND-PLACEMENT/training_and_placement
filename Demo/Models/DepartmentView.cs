using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class DepartmentView
    {
        [AllowNull]
        public IEnumerable<Department> Departments { get; set; }

        public Department department { get; set; }
    }
}
