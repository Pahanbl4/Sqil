using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public Roles Roles { get; set; }
        public int RolesId { get; set; }
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
    }
}
