using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model
{
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}
