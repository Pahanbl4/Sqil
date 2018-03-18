using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
