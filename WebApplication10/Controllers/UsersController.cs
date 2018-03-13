using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Core;
using WebApplication10.Data.Abstract;
using WebApplication10.Model;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        int page = 1;
        int pageSize = 4;

        public UsersController(IUsersRepository usersRepository,
                                  IEnrollmentRepository enrollmentRepository)
        {
            _usersRepository = usersRepository;
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
            var totalStudents = await _usersRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            IEnumerable<User> students = _usersRepository
                .GetAll()
                .OrderBy(s => s.ID)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalStudents, totalPages);

            IEnumerable<UserViewModel> studentsVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(students);

            return new OkObjectResult(studentsVM);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return NotFound();

            User user = await _usersRepository.GetUserAsync(id);

            if (user != null)
            {
                UserDetailsViewModel userDetailsVM = Mapper.Map<User, UserDetailsViewModel>(user);

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
                EnrollmentDate = user.EnrollmentDate
            };

            await _usersRepository.AddAsync(newUser);
            await _usersRepository.CommitAsync();

            user = Mapper.Map<User, UserViewModel>(newUser);

            return CreatedAtRoute("GetUser", new { controller = "Users", id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]UserViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == null)
                return NotFound();

            User updateStudent = _usersRepository.GetSingle(s => s.ID == id);

            if (updateStudent == null)
                return NotFound();
            else
            {
                updateStudent.LastName = student.LastName;
                updateStudent.FirstMidName = student.FirstMidName;
                updateStudent.EnrollmentDate = student.EnrollmentDate;

                await _usersRepository.CommitAsync();
            }

            //student = Mapper.Map<Student, StudentViewModel>(updateStudent);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteUser = _usersRepository.GetSingle(s => s.ID == id);

            if (deleteUser == null)
                return new NotFoundResult();
            else
            {
                IEnumerable<Enrollment> enrollments = _enrollmentRepository.FindBy(e => e.UserID == id);

                foreach (var enrollment in enrollments)
                    _enrollmentRepository.Delete(enrollment);

                _usersRepository.Delete(deleteUser);

                await _usersRepository.CommitAsync();

                return new NoContentResult();
            }
        }
    }
}
