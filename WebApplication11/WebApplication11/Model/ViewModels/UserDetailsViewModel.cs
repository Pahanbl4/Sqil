using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model.ViewModels
{
    public class UserDetailsViewModel
    {
        public int ID { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}
