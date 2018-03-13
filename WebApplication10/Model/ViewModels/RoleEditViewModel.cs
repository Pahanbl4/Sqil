using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model.Interfaces;

namespace WebApplication10.Model.ViewModels
{
    public class RoleEditViewModel:IRole
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<AssignedPermissionViewModel> AssignedCourses { get; set; }
    }
}
