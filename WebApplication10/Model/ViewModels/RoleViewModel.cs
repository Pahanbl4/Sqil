using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Model.Validations;

namespace WebApplication10.Model.ViewModels
{
    public class RoleViewModel: IValidatableObject
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string[] SelectedPermissions { get; set; }

        public ICollection<PermissionViewModel> Permissions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RoleViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
