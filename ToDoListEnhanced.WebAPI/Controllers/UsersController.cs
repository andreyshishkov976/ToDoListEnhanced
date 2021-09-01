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
using ToDoListEnhanced.WebAPI.Authentication;
using ToDoListEnhanced.ApiBLL.DTO;
using ToDoListEnhanced.ApiBLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListEnhanced.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [Authorize]
        [HttpGet("Get")]
        public async Task<ICollection<UserDTO>> GetAll()
        {
            return await _userService.Get();
        }

        [AllowAnonymous]
        [HttpGet("Get/{login}")]
        public async Task<ICollection<UserDTO>> GetByLogin(string login)
        {
            return await _userService.Get(login);
        }

        [HttpPost("Authorize/{login}&{password}")]
        public async Task<IActionResult> Token(string login, string password)
        {
            var identity = await _userService.Authorize(login,password);
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

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost("Register")]
        public void RegisterUser([FromBody] UserDTO userDto)
        {
            _userService.Create(userDto);
        }

        // PUT api/<UsersController>
        [Authorize]
        [HttpPut("Update")]
        public void UpdateUser([FromBody] UserDTO userDto)
        {
            _userService.Update(userDto);
        }

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public void DeleteUser(Guid id)
        {
            _userService.Delete(id);
        }
    }
}
