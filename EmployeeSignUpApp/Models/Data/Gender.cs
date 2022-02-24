using System;
using System.Collections.Generic;

namespace EmployeeSignUpApp.Models.Data
{
    public partial class Gender
    {
        public Gender()
        {
            Employees = new HashSet<Employee>();
        }

        public long GenderId { get; set; }
        public string GenderDesc { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}