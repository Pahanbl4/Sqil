using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication10.Core;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;
        int page = 1;
        int pageSize = 4;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
           
        }

        public async Task<IActionResult> Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalInstructors = await _rolesRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalInstructors / pageSize);

            IEnumerable<Role> roles = _rolesRepository
                .GetAllRoles()
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalInstructors, totalPages);

            IEnumerable<RoleViewModel> coursesVM = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roles);

            return new OkObjectResult(coursesVM);
        }

        [HttpGet("{id}", Name = "GetRole")]
        public IActionResult Get(int id)
        {
            Role role = _rolesRepository
                .GetSingle(i => i.ID == id, i => i.PermissionAssignments);

            if (role != null)
            {
                RoleEditViewModel roleDetailsVM = Mapper.Map<Role, RoleEditViewModel>(role);
                roleDetailsVM.AssignedCourses = _rolesRepository.GetAssignedPermissions(role);

                return new OkObjectResult(roleDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/roles", Name = "GetRoleCourses")]
        public async Task<IActionResult> GetInstCrs(int id)
        {
            Role role = await _rolesRepository.GetRolePermissions(id);

            if (role != null)
            {
                RoleDetailsViewModel roleDetailsVM = Mapper.Map<Role, RoleDetailsViewModel>(role);

                return new OkObjectResult(roleDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/users", Name = "GetRolePermissionUsers")]
        public async Task<IActionResult> GetInstCrStds(int id)
        {
            Permission permisson = await _rolesRepository.GetEnrolledUsers(id);

            if (permisson != null)
            {
                RoleDetailsViewModel instructorDetailsVM = Mapper.Map<Permission, RoleDetailsViewModel>(permisson);

                return new OkObjectResult(instructorDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]RoleViewModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Role newRole = new Role
            {
                RoleName = role.RoleName
            };

           
            // One-to-Many
            if (role.SelectedPermissions != null)
            {
                newRole.PermissionAssignments = new List<PermissionAssignment>();
                foreach (var permission in role.SelectedPermissions)
                {
                    var permissionToAdd = new PermissionAssignment { RoleID = role.ID, PermissionID = int.Parse(permission) };
                    newRole.PermissionAssignments.Add(permissionToAdd);
                }
            }

            await _rolesRepository.AddAsync(newRole);
            await _rolesRepository.CommitAsync();

            // Need to grab Course (.ThenInclude(ca => ca.Course)) since newInstructor does not retrieve Course entity after CommitAsync() 
            // when InstructorViewModel maps (Title = ca.Course.Title)          
            Role RoleWithPermission = await _rolesRepository.GetRolePermissions(newRole.ID);

            role = Mapper.Map<Role, RoleViewModel>(RoleWithPermission);

            return CreatedAtRoute("GetRole", new { controller = "Role", id = role.ID }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RoleViewModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Role updateRole = _rolesRepository
                .GetSingle(i => i.ID == id, i => i.PermissionAssignments);

            if (updateRole == null)
                return NotFound();
            else
            {
         

                updateRole.RoleName = role.RoleName;
          

               

                _rolesRepository.UpdateRolePermissions(role.SelectedPermissions, updateRole);

                await _rolesRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Role deleteRole = _rolesRepository
                .GetSingle(i => i.ID == id, i => i.PermissionAssignments);
           

                _rolesRepository.Delete(deleteRole);

                await _rolesRepository.CommitAsync();

                return new NoContentResult();
            
        }
    }
}