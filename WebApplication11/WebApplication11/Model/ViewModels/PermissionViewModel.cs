using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Model.Validations;

namespace WebApplication11.Model.ViewModels
{
    public class PermissionViewModel: IValidatableObject
    {
        public int ID { get; set; }
        public string PermissionName { get; set; }
       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new PermissionViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
