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
    public class PermissionsController : Controller
    {
        private readonly IPermissionsRepository _permissionsRepository;
        int page = 1;
        int pageSize = 4;

        public PermissionsController(IPermissionsRepository permissionsRepository)
        {
            _permissionsRepository = permissionsRepository;
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
            var totalCourses = await _permissionsRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

            IEnumerable<Permission> permissions = _permissionsRepository
                .AllIncluding(c => c.Department)
                .OrderBy(c => c.PermissionID)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalCourses, totalPages);

            IEnumerable<PermissionViewModel> coursesVM = Mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(permissions);

            return new OkObjectResult(coursesVM);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IEnumerable<Permission> permissions = _permissionsRepository.GetAll();
            IEnumerable<PermissionViewModel> coursesVM = Mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(permissions);

            return new OkObjectResult(coursesVM);
        }

        [HttpGet("{id}", Name = "GetPermission")]
        public IActionResult Get(int? id)
        {
            if (id == null)
                return NotFound();

            Permission permission = _permissionsRepository.GetSingle(c => c.PermissionID == id, c => c.Department);

            if (permission != null)
            {
                PermissionViewModel courseVM = Mapper.Map<Permission, PermissionViewModel>(permission);

                return new OkObjectResult(courseVM);
            }
            else
                return NotFound();
        }

        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            IEnumerable<Department> department = _permissionsRepository.PopulateDepartmentDropdownList();

            if (department != null)
            {
                return new OkObjectResult(department);
            }
            else
                return StatusCode(500);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]PermissionViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Permission newPermission = new Permission
            {
                PermissionID = course.PermissionID,
                Title = course.Title,
                 DepartmentID = course.DepartmentID
            };

            await _permissionsRepository.AddAsync(newPermission);
            await _permissionsRepository.CommitAsync();

            course = Mapper.Map<Permission, PermissionViewModel>(newPermission);

            return CreatedAtRoute("GetPermission", new { controller = "Permissions", id = course.PermissionID }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]PermissionViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == null)
                return NotFound();

            Permission updatePermission = _permissionsRepository.GetSingle(s => s.PermissionID == id);

            if (updatePermission == null)
                return NotFound();
            else
            {
                updatePermission.Title = course.Title;
                updatePermission    .DepartmentID = course.DepartmentID;

                await _permissionsRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var deletePermission = _permissionsRepository.GetSingle(c => c.PermissionID == id, c => c.Department);

            if (deletePermission == null)
                return new NotFoundResult();
            else
            {
                _permissionsRepository.Delete(deletePermission);

                await _permissionsRepository.CommitAsync();

                return new NoContentResult();
            }
        }
    }
}