using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Data.Abstract;
using WebApplication11.Model;

namespace WebApplication11.Data.Repositories
{
    public class PermissionsRepository : EntityBaseRepository<Permission>, IPermissionsRepository
    {
        private ApplicationContext _context;

        public PermissionsRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Permission> GetPermissionAsync(int? id)
        {
            return await _context.Permissions
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
        }
    }
}
