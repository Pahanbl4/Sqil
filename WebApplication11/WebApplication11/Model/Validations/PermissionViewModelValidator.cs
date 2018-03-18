using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Model.Validations
{
    public class PermissionViewModelValidator: AbstractValidator<PermissionViewModel>
    {
        public PermissionViewModelValidator()
        {
            RuleFor(s => s.PermissionName).Length(1, 50).WithMessage("Last Name cannot be longer than 50 characters.");
      

            RuleFor(s => s.PermissionName).NotEmpty().WithMessage("Last Name cannot be empty.");
            
        }
    }
}
