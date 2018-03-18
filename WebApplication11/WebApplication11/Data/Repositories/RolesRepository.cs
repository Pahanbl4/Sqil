using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Data.Abstract;
using WebApplication11.Model;

namespace WebApplication11.Data.Repositories
{
    public class RolesRepository : EntityBaseRepository<Role>, IRolesRepository
    {
        private ApplicationContext _context;

        public RolesRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
