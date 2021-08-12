using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListEnhanced.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        IUnitOfWork Database;

        public ProjectsController(IUnitOfWork database)
        {
            Database = database;
        }

        // GET api/<ProjectsController>
        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<ICollection<ProjectDTO>> Get()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<ICollection<Project>, List<ProjectDTO>>(await Database.Projects.GetAll());
        }

        // GET: api/<ProjectsController>/{userId}
        [Authorize]
        [HttpGet("Get/{id}")]
        public async Task<ICollection<ProjectDTO>> Get(Guid id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<ICollection<Project>, List<ProjectDTO>>(await Database.Projects.Find(item => item.UserId == id));
        }

        // POST api/<ProjectsController>
        [Authorize]
        [HttpPost("Create")]
        public void Post([FromBody] ProjectDTO projectDto)
        {
            Project project = new Project
            {
                Id = projectDto.Id,
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                Status = projectDto.Status,
                UserId = projectDto.UserId,
                SubTasks = new List<SubTask>()
            };
            Database.Projects.Create(project);
            Database.SaveAsync();
        }

        // PUT api/<ProjectsController>/5
        [Authorize]
        [HttpPut("Update")]
        public void Put([FromBody] ProjectDTO projectDto)
        {
            Project project = Database.Projects.Get(projectDto.Id);
            project.ProjectName = projectDto.ProjectName;
            project.Description = projectDto.Description;
            project.Status = projectDto.Status;
            Database.Projects.Update(project);
            Database.SaveAsync();
        }

        // DELETE api/<ProjectsController>/5
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public void Delete(Guid id)
        {
            Database.Projects.Delete(id);
            Database.SaveAsync();
        }
    }
}
