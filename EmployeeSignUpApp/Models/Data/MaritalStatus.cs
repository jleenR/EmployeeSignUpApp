using System;
using System.Collections.Generic;

namespace EmployeeSignUpApp.Models.Data
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Employees = new HashSet<Employee>();
        }

        public long MaritalStatusId { get; set; }
        public string MaritalStatusDesc { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
