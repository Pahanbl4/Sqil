using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model.ViewModels;

namespace WebApplication11.Model.Validations
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(s => s.Title).Length(3, 50).WithMessage("Title must be longer than 3 characters and less than 50 characters.");
        }
    }
}
