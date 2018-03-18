using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Model.Interfaces
{
    interface IUser
    {
        int ID { get; set; }
        string LastName { get; set; }
        string FirstMidName { get; set; }
        DateTime HireDate { get; set; }
    }
}
