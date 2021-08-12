using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;
using ToDoListEnhanced.WebAPI.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListEnhanced.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUnitOfWork Database;
        private IPasswordHasher<UserDTO> _passwordHasher;

        public UsersController(IUnitOfWork database)
        {
            Database = database;
            _passwordHasher = new PasswordHasher<UserDTO>();
        }

        // GET: api/<UsersController>
        [Authorize]
        [HttpGet("Get")]
        public async Task<ICollection<UserDTO>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<ICollection<User>, List<UserDTO>>(await Database.Users.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("Get/{login}")]
        public async Task<ICollection<UserDTO>> GetByLogin(string login)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<ICollection<User>, List<UserDTO>>(await Database.Users.Find(item => item.Login == login));
        }

        [HttpPost("Authorize/{login}&{password}")]
        public async Task<IActionResult> Token(string login, string password)
        {
            var identity = await GetIdentity(login, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Неверный логин или пароль." });
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                userId = identity.Name.Split('.')[0],
                username = identity.Name.Split('.')[1]
            };
            return new JsonResult(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            UserDTO authorizedUser = null;
            var users = await GetByLogin(login);
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

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost("Register")]
        public void RegisterUser([FromBody] UserDTO userDto)
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

        // PUT api/<UsersController>
        [Authorize]
        [HttpPut("Update")]
        public void UpdateUser([FromBody] UserDTO userDto)
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

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public void DeleteUser(Guid id)
        {
            Database.Users.Delete(id);
            Database.SaveAsync();
        }
    }
}
