using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Model.Validations
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(s => s.RoleName).Length(1, 50).WithMessage("Name cannot be longer than 50 characters.");


            RuleFor(s => s.RoleName).NotEmpty().WithMessage(" Name cannot be empty.");
          
        }
    }
}
