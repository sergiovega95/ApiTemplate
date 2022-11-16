using ApiTemplate.Domain.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Domain.DomainServices.Authentication
{
    public interface IAuthenticationService
    {
        string GenerateJwt(UserDTO user);

    }
}
