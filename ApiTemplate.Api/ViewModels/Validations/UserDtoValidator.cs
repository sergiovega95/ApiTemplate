using ApiTemplate.Domain.DTOs.Authentication;
using FluentValidation;
using System;

namespace ApiTemplate.Api.ViewModels.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDTO>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name Required")
                .Length(2,20).WithMessage("Min 2 Max 20 characters to name property");


            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email Required")
                .MaximumLength(250).WithMessage("Max 250 characters to email property")
                .EmailAddress().WithMessage("Format Email Incorrect");
        }

    }  
        
}
