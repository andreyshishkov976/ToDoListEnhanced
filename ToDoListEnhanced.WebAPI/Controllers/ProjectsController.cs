using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListEnhanced.ApiBLL.DTO;
using ToDoListEnhanced.ApiBLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListEnhanced.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IDataService<ProjectDTO> _projectService;

        public ProjectsController(IDataService<ProjectDTO> projectService/*IUnitOfWork database*/)
        {
            _projectService = projectService;
        }

        // GET: api/<ProjectsController>/{userId}
        [Authorize]
        [HttpGet("Get/{id}")]
        public async Task<ICollection<ProjectDTO>> Get(Guid id)
        {
            return await _projectService.Get(id);
        }

        // POST api/<ProjectsController>
        [Authorize]
        [HttpPost("Create")]
        public void Post([FromBody] ProjectDTO projectDto)
        {
            //if (projectDto.UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
            _projectService.Create(projectDto);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        }

        // PUT api/<ProjectsController>/5
        [Authorize]
        [HttpPut("Update")]
        public void Put([FromBody] ProjectDTO projectDto)
        {
            //if (projectDto.UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
            _projectService.Update(projectDto);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        }

        // DELETE api/<ProjectsController>/5
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public void Delete(Guid id)
        {
            //if (Database.Projects.Get(id).UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
                _projectService.Delete(id);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        } 
    }
}
