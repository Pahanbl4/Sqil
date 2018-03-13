using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int? RoleID { get; set; }

        public Role Administrator { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
