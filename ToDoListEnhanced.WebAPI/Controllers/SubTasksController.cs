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
    public class SubTasksController : ControllerBase
    {
        IUnitOfWork Database;

        public SubTasksController(IUnitOfWork database)
        {
            Database = database;
        }

        // GET: api/<SubTasksController>
        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<ICollection<SubTaskDTO>> Get()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubTask, SubTaskDTO>()).CreateMapper();
            return mapper.Map<ICollection<SubTask>, List<SubTaskDTO>>(await Database.SubTasks.GetAll());
        }

        // GET api/<SubTasksController>/5
        [Authorize]
        [HttpGet("Get/{id}")]
        public async Task<ICollection<SubTaskDTO>> Get(Guid id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubTask, SubTaskDTO>()).CreateMapper();
            return mapper.Map<ICollection<SubTask>, List<SubTaskDTO>>(await Database.SubTasks.Find(item => item.ProjectId == id));
        }

        // POST api/<SubTasksController>
        [Authorize]
        [HttpPost("Create")]
        public void Post([FromBody] SubTaskDTO subTaskDto)
        {
            SubTask subTask = new SubTask
            {
                Id = subTaskDto.Id,
                SubTaskName = subTaskDto.SubTaskName,
                Description = subTaskDto.Description,
                Status = subTaskDto.Status,
                ProjectId = subTaskDto.ProjectId
            };
            Database.SubTasks.Create(subTask);
            Database.SaveAsync();
        }

        // PUT api/<SubTasksController>/5
        [HttpPut("Update")]
        public void Put([FromBody] SubTaskDTO subTaskDto)
        {
            SubTask subTask = Database.SubTasks.Get(subTaskDto.Id);
            subTask.SubTaskName = subTaskDto.SubTaskName;
            subTask.Description = subTaskDto.Description;
            subTask.Status = subTaskDto.Status;
            Database.SubTasks.Update(subTask);
            Database.SaveAsync();
        }

        // DELETE api/<SubTasksController>/5
        [HttpDelete("Delete/{id}")]
        public void Delete(Guid id)
        {
            Database.SubTasks.Delete(id);
            Database.SaveAsync();
        }
    }
}
