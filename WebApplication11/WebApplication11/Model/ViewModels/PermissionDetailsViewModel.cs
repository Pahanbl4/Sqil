using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model.ViewModels
{
    public class PermissionDetailsViewModel
    {
        public int ID { get; set; }
        public string PermissionName { get; set; }


        public ICollection<EnrollmentViewModel> Enrollments { get; set; }
    }
}
