using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model
{
    public class Permission
    {
        public int ID { get; set; }
        public string PermissionName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
