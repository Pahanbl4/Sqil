using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model.ViewModels
{
    public class AssignedRoleViewModel
    {
        public int RoleID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}
