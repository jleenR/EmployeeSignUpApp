using System;
using System.Collections.Generic;

namespace EmployeeSignUpApp.Models.Data
{
    public partial class Credential
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}