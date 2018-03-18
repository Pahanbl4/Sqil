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
    public class PermissionsController : Controller
    {
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        int page = 1;
        int pageSize = 4;

        public PermissionsController(IPermissionsRepository studentsRepository,
                                  IEnrollmentRepository enrollmentRepository)
        {
            _permissionsRepository = studentsRepository;
            _enrollmentRepository = enrollmentRepository;
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
            var totalStudents = await _permissionsRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            IEnumerable<Permission> permissions = _permissionsRepository
                .GetAll()
                .OrderBy(s => s.ID)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalStudents, totalPages);

            IEnumerable<PermissionViewModel> permissionsVM = Mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(permissions);

            return new OkObjectResult(permissionsVM);
        }

        [HttpGet("{id}", Name = "GetPermission")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return NotFound();

            Permission permission = await _permissionsRepository.GetPermissionAsync(id);

            if (permission != null)
            {
                PermissionDetailsViewModel permissionDetailsVM = Mapper.Map<Permission, PermissionDetailsViewModel>(permission);

                return new OkObjectResult(permissionDetailsVM);
            }
            else
                return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]PermissionViewModel permission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Permission newPermission = new Permission
            {
                PermissionName = permission.PermissionName
            };

            await _permissionsRepository.AddAsync(newPermission);
            await _permissionsRepository.CommitAsync();

            permission = Mapper.Map<Permission, PermissionViewModel>(newPermission);

            return CreatedAtRoute("GetPermission", new { controller = "Permissions", id = permission.ID }, permission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]PermissionViewModel permission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == null)
                return NotFound();

            Permission updatePermission = _permissionsRepository.GetSingle(s => s.ID == id);

            if (updatePermission == null)
                return NotFound();
            else
            {
                updatePermission.PermissionName = permission.PermissionName;
               
                await _permissionsRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var deletePermission = _permissionsRepository.GetSingle(s => s.ID == id);

            if (deletePermission == null)
                return new NotFoundResult();
            else
            {
                IEnumerable<Enrollment> enrollments = _enrollmentRepository.FindBy(e => e.PermissionID == id);

                foreach (var enrollment in enrollments)
                    _enrollmentRepository.Delete(enrollment);

                _permissionsRepository.Delete(deletePermission);

                await _permissionsRepository.CommitAsync();

                return new NoContentResult();
            }
        }
    }
}