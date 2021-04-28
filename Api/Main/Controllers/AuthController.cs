using Microsoft.AspNetCore.Mvc;
using System;

using Catalyst.Api.Main.Exceptions;
using Catalyst.Api.Main.Models;
using Catalyst.Api.Main.Services;

namespace Catalyst.Api.Main.Controllers
{
    public class LoginResult
    {
        public string Token { get; set; }
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ProposedUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private AuthService _authService;

        public ApiController(AuthService authService) => _authService = authService;

        [HttpPost("login")]
        public IActionResult Login(Credentials credentials)
        {
            IActionResult response;

            try
            {
                string token = _authService.Login(credentials.Email, credentials.Password);

                response = Ok(new LoginResult { Token = token });
            }
            catch (Exception)
            {
                response = Unauthorized(new ErrorResult { Message = "Unable to login." });
            }

            return response;
        }

        [HttpPost("register")]
        public IActionResult Register(ProposedUser user)
        {
            IActionResult response;

            if (user.Password != user.ConfirmedPassword)
            {
                return BadRequest("Password confirmation does not match password.");
            }

            try
            {
                string token = _authService.Register(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                });

                response = Ok(new LoginResult { Token = token });
            }
            catch (RecordExistsException)
            {
                response = BadRequest(new ErrorResult { Message = "User already exists." });
            }

            return response;
        }
    }
}