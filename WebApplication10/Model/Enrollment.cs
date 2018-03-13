using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model
{
    public enum Lavel
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
            public int EnrollmentID { get; set; }
            public int PermissionID { get; set; }
            public int UserID { get; set; }
            public Lavel? Lavel { get; set; }

            public Permission Permission { get; set; }
            public User User { get; set; }
        
    }
}
