using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model
{
    public class RoleAssignment
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
