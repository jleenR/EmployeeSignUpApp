using System;
using System.Collections.Generic;

namespace EmployeeSignUpApp.Models.Data
{
    public partial class Employee
    {
        public long EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long GenderId { get; set; }
        public long MaritalStatusId { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual MaritalStatus MaritalStatus { get; set; } = null!;
        public virtual Credential Credential { get; set; } = null!;
    }
}
