using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Data.Abstract;
using WebApplication11.Model;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Data.Repositories
{
    public class UsersRepository : EntityBaseRepository<User>, IUsersRepository
    {
        private ApplicationContext _context;

        public UsersRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result = _context.Users
                  .Include(i => i.RoleAssignments)
                    .ThenInclude(i => i.Role)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName);

            return result;
        }

        public async Task<User> GetUserRoles(int id)
        {
            var result = await _context.Users
                  .Include(i => i.RoleAssignments)
                    .ThenInclude(ca => ca.Role)
                  .Where(i => i.ID == id)
                  .AsNoTracking()
                  .OrderBy(i => i.RoleAssignments.Select(c => c.RoleID))
                  .SingleOrDefaultAsync();

            return result;
        }

        public async Task<Role> GetEnrolledPermissions(int roleId)
        {
            var result = await _context.Roles
                   .Include(i => i.Enrollments)
                     .ThenInclude(e => e.Permission)
                   .Where(i => i.RoleID == roleId)
                   .AsNoTracking()
                   .OrderBy(i => i.RoleID)
                   .SingleOrDefaultAsync();

            return result;
        }

        public List<AssignedRoleViewModel> GetAssignedRoles(User user)
        {
            var allRoles = _context.Roles;
            var userRoles = new HashSet<int>(user.RoleAssignments.Select(c => c.RoleID));
            var viewModel = new List<AssignedRoleViewModel>();

            foreach (var role in allRoles)
            {
                viewModel.Add(new AssignedRoleViewModel
                {
                    RoleID = role.RoleID,
                    Title = role.Title,
                    Assigned = userRoles.Contains(role.RoleID)
                });
            }

            return viewModel;
        }

        public void UpdateUserRoles(string[] selectedRoles, User userToUpdate)
        {
            if (selectedRoles == null)
            {
                userToUpdate.RoleAssignments = new List<RoleAssignment>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var userRoles = new HashSet<int>
                (userToUpdate.RoleAssignments.Select(c => c.RoleID));

            foreach (var role in _context.Roles)
            {
                if (selectedRolesHS.Contains(role.RoleID.ToString()))
                {
                    if (!userRoles.Contains(role.RoleID))
                    {
                        userToUpdate.RoleAssignments.Add(new RoleAssignment { UserID = userToUpdate.ID, RoleID = role.RoleID });
                    }
                }
                else
                {
                    if (userRoles.Contains(role.RoleID))
                    {
                        RoleAssignment roleToRemove = userToUpdate.RoleAssignments.SingleOrDefault(i => i.RoleID == role.RoleID);
                        _context.Remove(roleToRemove);
                    }
                }
            }
        }

       
    }
}
