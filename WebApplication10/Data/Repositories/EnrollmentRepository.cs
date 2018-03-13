using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;

namespace WebApplication10.Data.Repositories
{
    public class EnrollmentRepository:EntityBaseRepository<Enrollment>, IEnrollmentRepository
    {
        private AplicationContext _context;

        public EnrollmentRepository(AplicationContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
