using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Data.Abstract
{
    

    public interface IPermissionsRepository : IEntityBaseRepository<Permission>
    {
        Task<Permission> GetPermissionAsync(int? id);
    }

    public interface IRolesRepository : IEntityBaseRepository<Role>
    {
       
    }

    public interface IUsersRepository : IEntityBaseRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserRoles(int id);
        Task<Role> GetEnrolledPermissions(int roleId);
        List<AssignedRoleViewModel> GetAssignedRoles(User user);
        void UpdateUserRoles(string[] selectedRoles, User userToUpdate);
    }


    public interface IEnrollmentRepository : IEntityBaseRepository<Enrollment> { }

}
