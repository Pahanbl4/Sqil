using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model.ViewModels
{
    public class RoleDetailsViewModel
    {
        public int ID { get; set; }

        public IEnumerable<PermissionViewModel> Courses { get; set; }
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}
