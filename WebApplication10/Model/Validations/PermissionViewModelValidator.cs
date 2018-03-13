using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Model.Validations
{
    public class PermissionViewModelValidator : AbstractValidator<PermissionViewModel>
    {
        public PermissionViewModelValidator()
        {
            RuleFor(s => s.Title).Length(3, 50).WithMessage("Title must be longer than 3 characters and less than 50 characters.");
        }
    }
}
