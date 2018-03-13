using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;

namespace WebApplication10.Data.Repositories
{
    public class DepartmentsRepository : EntityBaseRepository<Department>, IDepartmentsRepository
    {
        private AplicationContext _context;

        public DepartmentsRepository(AplicationContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
