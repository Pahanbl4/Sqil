using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }
        public string Title { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}
