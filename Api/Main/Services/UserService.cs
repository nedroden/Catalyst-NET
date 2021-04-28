using System;
using System.Collections.Generic;
using System.Linq;
using Catalyst.Api.Main.Models;
using Catalyst.Api.Main.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Catalyst.Api.Main.Services
{
    public class UserService : IService<User>
    {
        private readonly ProgramContext _programContext;

        public UserService(ProgramContext programContext) => _programContext = programContext;

        public IEnumerable<User> FetchAll()
        {
            return _programContext.Users.ToList();
        }

        public User FetchSingle(long id)
        {
            return _programContext.Users.Where(user => user.Id == id).FirstOrDefault();
        }

        public User FetchByEmail(string email)
        {
            return _programContext.Users.Where(user => user.Email == email).FirstOrDefault();
        }

        public User FetchByUsername(string username)
        {
            return _programContext.Users.Where(user => user.Name == username).FirstOrDefault();
        }

        public User Create(User user)
        {
            bool emailInUse = FetchByEmail(user.Email) != null;
            bool usernameInUse = FetchByUsername(user.Name) != null;

            if (emailInUse || usernameInUse)
            {
                var field = emailInUse ? "Email address" : "Username";

                throw new RecordExistsException($"{field} is already in use.");
            }

            _programContext.Users.Add(user);
            _programContext.SaveChanges();

            return user;
        }

        public void Update(User user)
        {
            if (!Exists(user.Id))
            {
                throw new KeyNotFoundException("User not found.");
            }

            _programContext.Users.Update(user);
            _programContext.SaveChanges();
        }

        public void Remove(long id)
        {
            Remove(new User { Id = id });
        }

        public void Remove(User user)
        {
            if (!Exists(user.Id))
            {
                throw new KeyNotFoundException("User not found.");
            }

            if (_programContext.Users.Count() == 1)
            {
                throw new ArgumentException("Cannot delete the last account.");
            }

            _programContext.Users.Remove(user);
            _programContext.SaveChanges();
        }

        public bool Exists(long id)
        {
            return FetchSingle(id) != null;
        }

        public bool Exists(string email, string username)
        {
            return FetchByEmail(email) is object || FetchByUsername(username) is object;
        }

        public bool IsValidPassword(User user, string plainPassword)
        {
            var hasher = new PasswordHasher<User>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, plainPassword);

            return result == PasswordVerificationResult.Success;
        }

        public string HashPassword(string plainPassword)
        {
            return new PasswordHasher<User>().HashPassword(null, plainPassword);
        }
    }
}