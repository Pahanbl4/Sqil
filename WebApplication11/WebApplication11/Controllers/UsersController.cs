using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Core;
using WebApplication11.Data.Abstract;
using WebApplication11.Model;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
      
        int page = 1;
        int pageSize = 4;

        public UsersController(IUsersRepository instructorsRepository)
        {
            _usersRepository = instructorsRepository;
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
            var totalInstructors = await _usersRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalInstructors / pageSize);

            IEnumerable<User> users = _usersRepository
                .GetAllUsers()
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalInstructors, totalPages);

            IEnumerable<UserViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

            return new OkObjectResult(usersVM);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            User user = _usersRepository
                .GetSingle(i => i.ID == id, i => i.RoleAssignments);

            if (user != null)
            {
                UserEditViewModel userDetailsVM = Mapper.Map<User, UserEditViewModel>(user);
                userDetailsVM.AssignedRoles = _usersRepository.GetAssignedRoles(user);

                return new OkObjectResult(userDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/roles", Name = "GetUserRoles")]
        public async Task<IActionResult> GetInstCrs(int id)
        {
            User user = await _usersRepository.GetUserRoles(id);

            if (user != null)
            {
                UserDetailsViewModel userDetailsVM = Mapper.Map<User, UserDetailsViewModel>(user);

                return new OkObjectResult(userDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/permissions", Name = "GetUserRolePermissions")]
        public async Task<IActionResult> GetInstCrStds(int id)
        {
            Role role = await _usersRepository.GetEnrolledPermissions(id);

            if (role != null)
            {
                UserDetailsViewModel userDetailsVM = Mapper.Map<Role, UserDetailsViewModel>(role);

                return new OkObjectResult(userDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User newUser = new User
            {
                LastName = user.LastName,
                FirstMidName = user.FirstMidName,
                HireDate = user.HireDate
            };

            

            // One-to-Many
            if (user.SelectedRoles != null)
            {
                newUser.RoleAssignments = new List<RoleAssignment>();
                foreach (var role in user.SelectedRoles)
                {
                    var roleToAdd = new RoleAssignment { UserID = user.ID, RoleID = int.Parse(role) };
                    newUser.RoleAssignments.Add(roleToAdd);
                }
            }

            await _usersRepository.AddAsync(newUser);
            await _usersRepository.CommitAsync();

            // Need to grab Course (.ThenInclude(ca => ca.Course)) since newInstructor does not retrieve Course entity after CommitAsync() 
            // when InstructorViewModel maps (Title = ca.Course.Title)          
            User UserWithRole = await _usersRepository.GetUserRoles(newUser.ID);

            user = Mapper.Map<User, UserViewModel>(UserWithRole);

            return CreatedAtRoute("GetUser", new { controller = "Users", id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User updateUser = _usersRepository
                .GetSingle(i => i.ID == id, i => i.RoleAssignments);

            if (updateUser == null)
                return NotFound();
            else
            {
               

                updateUser.LastName = user.LastName;
                updateUser.FirstMidName = user.FirstMidName;
                updateUser.HireDate = user.HireDate;

               

                _usersRepository.UpdateUserRoles(user.SelectedRoles, updateUser);

                await _usersRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User deleteUser = _usersRepository
                .GetSingle(i => i.ID == id, i => i.RoleAssignments);
            

            if (deleteUser == null)
                return new NotFoundResult();
            else
            {
                _usersRepository.Delete(deleteUser);

                await _usersRepository.CommitAsync();

                return new NoContentResult();
            }
        }
    }
}