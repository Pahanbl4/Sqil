using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public ICollection<PermissionAssignment> PermissionAssignments { get; set; }
        
    }
}
