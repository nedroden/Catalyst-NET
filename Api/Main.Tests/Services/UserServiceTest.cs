using System;
using Xunit;

using Catalyst.Api.Main.Models;
using Catalyst.Api.Main.Services;
using Microsoft.EntityFrameworkCore;

namespace Main.Tests.Services
{
    public class UserServiceTest
    {
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService(new ProgramContext(new DbContextOptions<ProgramContext>()));
        }

        [Fact]
        public void Hasher_Should_Hash_Password()
        {
            var plainText = "123456";

            Assert.NotEqual(_userService.HashPassword(plainText), plainText);
        }
    }
}
