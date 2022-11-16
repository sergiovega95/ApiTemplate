using ApiTemplate.Domain.DomainServices.Authentication;
using ApiTemplate.Domain.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation;
using FluentValidation.Results;

namespace ApiTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase {

        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IValidator<UserDTO> _validator;

        public AuthenticationController(IAuthenticationService authenticationService , 
                                        ILogger<AuthenticationController> logger,
                                        IValidator<UserDTO> validator) { 

            _logger= logger;
            _authenticationService= authenticationService;
            _validator = validator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetAuthToken([FromBody] UserDTO user) {
            
            await _validator.ValidateAndThrowAsync(user);            

            return Ok(_authenticationService.GenerateJwt(user));
        }      
                
    }
}
