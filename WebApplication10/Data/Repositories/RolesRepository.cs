using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Data.Repositories
{
    public class RolesRepository : EntityBaseRepository<Role>, IRolesRepository
    {
        private AplicationContext _context;

        public RolesRepository(AplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<Role> result = _context.Roles
                  .Include(i => i.PermissionAssignments)
                    .ThenInclude(i => i.Permission)
                  .AsNoTracking()
                  .OrderBy(i => i.RoleName);

            return result;
        }

        public async Task<Role> GetRolePermissions(int id)
        {
            var result = await _context.Roles
                  .Include(i => i.PermissionAssignments)
                    .ThenInclude(ca => ca.Permission)
                    .ThenInclude(c => c.Department)
                  .Where(i => i.ID == id)
                  .AsNoTracking()
                  .OrderBy(i => i.PermissionAssignments.Select(c => c.PermissionID))
                  .SingleOrDefaultAsync();

            return result;
        }

        public async Task<Permission> GetEnrolledUsers(int courseId)
        {
            var result = await _context.Permissions
                   .Include(i => i.Enrollments)
                     .ThenInclude(e => e.User)
                   .Where(i => i.PermissionID == courseId)
                   .AsNoTracking()
                   .OrderBy(i => i.PermissionID)
                   .SingleOrDefaultAsync();

            return result;
        }

        public List<AssignedPermissionViewModel> GetAssignedPermissions(Role role)
        {
            var allPermissions = _context.Permissions;
            var instructorCourses = new HashSet<int>(role.PermissionAssignments.Select(c => c.PermissionID));
            var viewModel = new List<AssignedPermissionViewModel>();

            foreach (var permissions in allPermissions)
            {
                viewModel.Add(new AssignedPermissionViewModel
                {
                    PermissionID = permissions.PermissionID,
                    Title = permissions.Title,
                    Assigned = instructorCourses.Contains(permissions.PermissionID)
                });
            }

            return viewModel;
        }

        public void UpdateRolePermissions(string[] selectedPermissions, Role roleToUpdate)
        {
            if (selectedPermissions == null)
            {
                roleToUpdate.PermissionAssignments = new List<PermissionAssignment>();
                return;
            }

            var selectedPermissionsHS = new HashSet<string>(selectedPermissions);
            var rolePermissions = new HashSet<int>
                (roleToUpdate.PermissionAssignments.Select(c => c.PermissionID));

            foreach (var permissions in _context.Permissions)
            {
                if (selectedPermissionsHS.Contains(permissions.PermissionID.ToString()))
                {
                    if (!rolePermissions.Contains(permissions.PermissionID))
                    {
                        roleToUpdate.PermissionAssignments.Add(new PermissionAssignment { RoleID = roleToUpdate.ID, PermissionID = permissions.PermissionID });
                    }
                }
                else
                {
                    if (rolePermissions.Contains(permissions.PermissionID))
                    {
                        PermissionAssignment courseToRemove = roleToUpdate.PermissionAssignments.SingleOrDefault(i => i.PermissionID == permissions.PermissionID);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }

       
    }
}
