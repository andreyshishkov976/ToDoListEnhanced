using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;
using ToDoListEnhanced.ApiBLL.DTO;
using ToDoListEnhanced.ApiBLL.Interfaces;

namespace ToDoListEnhanced.ApiBLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database;
        private IPasswordHasher<UserDTO> _passwordHasher;

        public UserService(IUnitOfWork database)
        {
            Database = database;
            _passwordHasher = new PasswordHasher<UserDTO>();
        }

        public async Task<ClaimsIdentity> Authorize(string login, string password)
        {
            UserDTO authorizedUser = null;
            var users = await Get(login);
            if (users == null)
                return null;
            else
            {
                PasswordVerificationResult verificationResult;
                foreach (var user in users)
                {
                    verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        authorizedUser = user;
                        break;
                    }
                }
            }
            if (authorizedUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, $@"{authorizedUser.Id}.{authorizedUser.LastName} {authorizedUser.FirstName} {authorizedUser.SurName}"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, authorizedUser.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public void Delete(Guid id)
        {
            Database.Users.Delete(id);
            Database.SaveAsync();
        }

        public async Task<ICollection<UserDTO>> Get()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<ICollection<User>, List<UserDTO>>(await Database.Users.GetAll());
        }

        public async Task<ICollection<UserDTO>> Get(string login)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<ICollection<User>, List<UserDTO>>(await Database.Users.Find(item => item.Login == login));
        }

        public void Create(UserDTO userDto)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                LastName = userDto.LastName,
                FirstName = userDto.FirstName,
                SurName = userDto.SurName,
                Login = userDto.Login,
                Projects = new List<Project>()
            };
            user.PasswordHash = _passwordHasher.HashPassword(userDto, userDto.PasswordHash);
            Database.Users.Create(user);
            Database.SaveAsync();
        }

        public void Update(UserDTO userDto)
        {
            User user = Database.Users.Get(userDto.Id);
            user.LastName = userDto.LastName;
            user.FirstName = userDto.FirstName;
            user.SurName = userDto.SurName;
            user.Login = userDto.Login;
            user.PasswordHash = _passwordHasher.HashPassword(userDto, userDto.PasswordHash);
            Database.Users.Update(user);
            Database.SaveAsync();
        }
    }
}
