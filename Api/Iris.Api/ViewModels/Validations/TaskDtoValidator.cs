using FluentValidation;
using Iris.Domain.DTOs.Tasks;

namespace Iris.Api.ViewModels.Validations
{
    public class TaskDtoValidator: AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(x => x.TaskDescription)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("TaskDescription Required")
                .MaximumLength(250).WithMessage("Max 250 characters TaskDescription field");

            RuleFor(x => x.TaskId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0")
                .NotEmpty().WithMessage("TaskId is required");               
                
        }

    }
}
