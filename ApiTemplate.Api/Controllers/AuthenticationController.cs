using ApiTemplate.Domain.DomainServices.Authentication;
using ApiTemplate.Domain.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase {

        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService , ILogger<AuthenticationController> logger) { 

            _logger= logger;
            _authenticationService= authenticationService;
        }

        [HttpPost("token")]
        public IActionResult GetAuthToken([FromBody] UserDTO user) {

            return Ok(_authenticationService.GenerateJwt(user));
        }      
                
    }
}
