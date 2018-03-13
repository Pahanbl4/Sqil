using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model.ViewModels;

namespace WebApplication10.Model.Validations
{
    public class UserViewModelValidator:AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(s => s.LastName).Length(1, 50).WithMessage("Last Name cannot be longer than 50 characters.");
            RuleFor(s => s.FirstMidName).Length(1, 50).WithMessage("First Name cannot be longer than 50 characters.");

            RuleFor(s => s.LastName).NotEmpty().WithMessage("Last Name cannot be empty.");
            RuleFor(s => s.FirstMidName).NotEmpty().WithMessage("First Name cannot be empty.");
        }
    }
}
