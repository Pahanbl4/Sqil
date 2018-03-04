using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModel
{
    public class UserView
    {
        public int UsersId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public string RolesName { get; set; }
        public string PermissionName { get; set; }
    }
}
