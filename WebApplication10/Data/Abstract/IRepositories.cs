using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Data.Abstract
{
    

    public interface IUsersRepository : IEntityBaseRepository<User>
    {
        Task<User> GetUserAsync(int? id);
    }

    public interface IPermissionsRepository : IEntityBaseRepository<Permission>
    {
        IEnumerable<Department> PopulateDepartmentDropdownList();
    }

    public interface IRolesRepository : IEntityBaseRepository<Role>
    {
        IEnumerable<Role> GetAllRoles();
        Task<Role> GetRolePermissions(int id);
        Task<Permission> GetEnrolledUsers(int courseId);
        List<AssignedPermissionViewModel> GetAssignedPermissions(Role instructor);
        void UpdateRolePermissions(string[] selectedCourses, Role instructorToUpdate);
    }


    public interface IEnrollmentRepository : IEntityBaseRepository<Enrollment> { }
    public interface IDepartmentsRepository : IEntityBaseRepository<Department> { }

}
