using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Model.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // UsersController
            CreateMap<User, UserDetailsViewModel>()
               .ForMember(vm => vm.Enrollments,
                    opt => opt.MapFrom(s => s.Enrollments.Select(e => new EnrollmentViewModel
                    {
                        Title = e.Permission.Title,
                        Lavel = ((Lavel)e.Lavel).ToString()
                    })));
            CreateMap<User, UserViewModel>();

            // PermissionsController
            CreateMap<Permission, PermissionViewModel>()
                .ForMember(vm => vm.Department, opt => opt.MapFrom(c => c.Department.Name))
                .ForMember(vm => vm.DepartmentID, opt => opt.MapFrom(c => c.DepartmentID))
                .ForMember(vm => vm.Assigned, opt => opt.UseValue(false));

            // RolesController
            CreateMap<Role, RoleViewModel>()
                .ForMember(vm => vm.Permissions, opt => opt.MapFrom(i => i.PermissionAssignments.Select(ca => new PermissionViewModel
                {
                    PermissionID = ca.PermissionID,
                    Title = ca.Permission.Title
                })));
            CreateMap<Role, RoleEditViewModel>();
            CreateMap<Role, RoleDetailsViewModel>()
               .ForMember(vm => vm.Courses,
                    opt => opt.MapFrom(i => i.PermissionAssignments.Select(ca => new PermissionViewModel
                    {
                        PermissionID = ca.Permission.PermissionID,
                        Title = ca.Permission.Title,
                        Department = ca.Permission.Department.Name
                    })));
            CreateMap<Permission, RoleDetailsViewModel>()
               .ForMember(vm => vm.Enrollments,
                    opt => opt.MapFrom(c => c.Enrollments.Select(e => new EnrollmentViewModel
                    {
                        FullName = e.User.FullName,
                        Lavel = ((Lavel)e.Lavel).ToString()
                    })));


            // ViewModel to Domain
            CreateMap<UserViewModel, User>();
            CreateMap<PermissionViewModel, Permission>();
            CreateMap<RoleViewModel, Role>();
        }
    }
}
