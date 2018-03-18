using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Model.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to ViewModel

            // PermissionController
            CreateMap<Permission, PermissionDetailsViewModel>()
               .ForMember(vm => vm.Enrollments,
                    opt => opt.MapFrom(s => s.Enrollments.Select(e => new EnrollmentViewModel
                    {
                        Title = e.Role.Title
                    })));
            CreateMap<Permission, PermissionViewModel>();

            // RolesController
            CreateMap<Role, RoleViewModel>()         
                .ForMember(vm => vm.Assigned, opt => opt.UseValue(false));

            // UsersController
            CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.Roles, opt => opt.MapFrom(i => i.RoleAssignments.Select(ca => new RoleViewModel
                {
                    RoleID = ca.RoleID,
                    Title = ca.Role.Title
                })));
            CreateMap<User, UserEditViewModel>();
               

            CreateMap<User, UserDetailsViewModel>()
               .ForMember(vm => vm.Roles,
                    opt => opt.MapFrom(i => i.RoleAssignments.Select(ca => new RoleViewModel
                    {
                        RoleID = ca.Role.RoleID,
                        Title = ca.Role.Title
                    })));

            CreateMap<Role, UserDetailsViewModel>()
               .ForMember(vm => vm.Enrollments,
                    opt => opt.MapFrom(c => c.Enrollments.Select(e => new EnrollmentViewModel
                    {
                        FullName = e.Permission.PermissionName
                    })));


            // ViewModel to Domain
            CreateMap<PermissionViewModel, Permission>();
            CreateMap<RoleViewModel, Role>();
            CreateMap<UserViewModel, User>();
        }
    }
}
