using ApiTemplate.Domain.DTOs.Authentication;
using FluentValidation;
using Iris.Domain.DTOs.Tasks;

namespace Iris.Api.ViewModels.Validations
{
    public class TaskRequestValidator : AbstractValidator<TaskRequest>
    {
        public TaskRequestValidator()
        {
            RuleFor(x => x.TaskDescription)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("TaskDescription Required")
                .MaximumLength(250).WithMessage("Max 250 characters TaskDescription field");
          
        }

    }  
    
}
