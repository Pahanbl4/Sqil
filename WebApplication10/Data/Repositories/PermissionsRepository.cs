using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;

namespace WebApplication10.Data.Repositories
{
    public class PermissionsRepository:EntityBaseRepository<Permission>, IPermissionsRepository
    {
        private AplicationContext _context;

        public PermissionsRepository(AplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Department> PopulateDepartmentDropdownList()
        {
            return _context.Departments.OrderBy(d => d.Name);
        }
    }
}
