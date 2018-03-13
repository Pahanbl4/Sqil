using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;

namespace WebApplication10.Data.Repositories
{
    public class UsersRepository: EntityBaseRepository<User>, IUsersRepository
    {
        private AplicationContext _context;

        public UsersRepository(AplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(int? id)
        {
            return await _context.Users
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Permission)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
        }
    }
}
