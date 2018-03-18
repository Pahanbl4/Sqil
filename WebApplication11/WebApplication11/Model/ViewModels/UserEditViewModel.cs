using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model.Interfaces;

namespace WebApplication11.Model.ViewModels
{
    public class UserEditViewModel:IUser
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }

        public IEnumerable<AssignedRoleViewModel> AssignedRoles { get; set; }
    }
}
