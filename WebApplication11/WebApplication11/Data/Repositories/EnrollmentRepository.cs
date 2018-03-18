using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Data.Abstract;
using WebApplication11.Model;

namespace WebApplication11.Data.Repositories
{
    public class EnrollmentRepository : EntityBaseRepository<Enrollment>, IEnrollmentRepository
    {
        private ApplicationContext _context;

        public EnrollmentRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
