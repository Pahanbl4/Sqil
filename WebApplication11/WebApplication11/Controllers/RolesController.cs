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
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;
        int page = 1;
        int pageSize = 4;

        public RolesController(IRolesRepository coursesRepository)
        {
            _rolesRepository = coursesRepository;
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
            var totalCourses = await _rolesRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

            IEnumerable<Role> roles = _rolesRepository
                .GetAll()
                .OrderBy(c => c.RoleID)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalCourses, totalPages);

            IEnumerable<RoleViewModel> coursesVM = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roles);

            return new OkObjectResult(coursesVM);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IEnumerable<Role> roles = _rolesRepository.GetAll();
            IEnumerable<RoleViewModel> rolesVM = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roles);

            return new OkObjectResult(rolesVM);
        }

        [HttpGet("{id}", Name = "GetRole")]
        public IActionResult Get(int? id)
        {
            if (id == null)
                return NotFound();

            Role role = _rolesRepository.GetSingle(c => c.RoleID == id);

            if (role != null)
            {
                RoleViewModel roleVM = Mapper.Map<Role, RoleViewModel>(role);

                return new OkObjectResult(roleVM);
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
                RoleID = role.RoleID,
                Title = role.Title
               
            };

            await _rolesRepository.AddAsync(newRole);
            await _rolesRepository.CommitAsync();

            role = Mapper.Map<Role, RoleViewModel>(newRole);

            return CreatedAtRoute("GetRole", new { controller = "Roles", id = role.RoleID }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]RoleViewModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == null)
                return NotFound();

            Role updateRole = _rolesRepository.GetSingle(s => s.RoleID == id);

            if (updateRole == null)
                return NotFound();
            else
            {
                updateRole.Title = role.Title;
               

                await _rolesRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteRole = _rolesRepository.GetSingle(c => c.RoleID == id);

            if (deleteRole == null)
                return new NotFoundResult();
            else
            {
                _rolesRepository.Delete(deleteRole);

                await _rolesRepository.CommitAsync();

                return new NoContentResult();
            }
        }
    }
}