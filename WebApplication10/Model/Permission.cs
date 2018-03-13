using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model
{
    public class Permission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PermissionID { get; set; }
        public string Title { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<PermissionAssignment> PermissionAssignments { get; set; }
    }
}
