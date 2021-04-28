using System;

using Catalyst.Api.Main.Exceptions;
using Catalyst.Api.Main.Models;

namespace Catalyst.Api.Main.Services
{
    public class AuthService
    {
        private UserService _userService;
        private TokenService _tokenService;

        public AuthService(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public string Login(string email, string plainPassword)
        {
            User user = _userService.FetchByEmail(email);

            if (user == null || !_userService.IsValidPassword(user, plainPassword))
            {
                throw new UnauthorizedAccessException();
            }

            return _tokenService.GenerateToken(user);
        }

        public string Register(User user)
        {
            if (_userService.Exists(user.Email, user.Name))
            {
                throw new RecordExistsException("User already exists");
            }

            user.Password = _userService.HashPassword(user.Password);
            _userService.Create(user);

            return _tokenService.GenerateToken(user);
        }
    }
}
