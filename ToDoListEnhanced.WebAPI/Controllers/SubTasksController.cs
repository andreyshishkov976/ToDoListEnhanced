using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListEnhanced.WebBLL.DTO;
using ToDoListEnhanced.WebBLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListEnhanced.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private IDataService<SubTaskDTO> _subTaskService;

        public SubTasksController(IDataService<SubTaskDTO> subTaskService)
        {
            _subTaskService = subTaskService;
        }

        // GET api/<SubTasksController>/5
        [Authorize]
        [HttpGet("Get/{id}")]
        public async Task<ICollection<SubTaskDTO>> Get(Guid id)
        {
            return await _subTaskService.Get(id);
        }

        // POST api/<SubTasksController>
        [Authorize]
        [HttpPost("Create")]
        public void Post([FromBody] SubTaskDTO subTaskDto)
        {
            //if (Database.Projects.Get(subTaskDto.ProjectId).UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
                _subTaskService.Create(subTaskDto);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        }

        // PUT api/<SubTasksController>/5
        [Authorize]
        [HttpPut("Update")]
        public void Put([FromBody] SubTaskDTO subTaskDto)
        {
            //if (Database.Projects.Get(subTaskDto.ProjectId).UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
                _subTaskService.Update(subTaskDto);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        }

        // DELETE api/<SubTasksController>/5
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public void Delete(Guid id)
        {
            //if (Database.Projects.Get(Database.SubTasks.Get(id).ProjectId).UserId == Guid.Parse(this.User.Identity.Name.Split('.')[0]))
            //{
                _subTaskService.Delete(id);
            //}
            //else this.BadRequest("У вас нет прав на это действие.");
        }
    }
}
