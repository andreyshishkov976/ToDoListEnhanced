using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;
using ToDoListEnhanced.ApiBLL.DTO;
using ToDoListEnhanced.ApiBLL.Interfaces;

namespace ToDoListEnhanced.ApiBLL.Services
{
    public class SubTaskService : IDataService<SubTaskDTO>
    {
        IUnitOfWork Database;

        public SubTaskService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(SubTaskDTO subTaskDto)
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

        public void Delete(Guid id)
        {
            Database.SubTasks.Delete(id);
            Database.SaveAsync();
        }

        public async Task<ICollection<SubTaskDTO>> Get(Guid? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubTask, SubTaskDTO>()).CreateMapper();
            return mapper.Map<ICollection<SubTask>, List<SubTaskDTO>>(await Database.SubTasks.Find(item => item.ProjectId == id));
        }

        public void Update(SubTaskDTO subTaskDto)
        {
            SubTask subTask = Database.SubTasks.Get(subTaskDto.Id);
            if (subTask != null)
            {
                subTask.SubTaskName = subTaskDto.SubTaskName;
                subTask.Description = subTaskDto.Description;
                subTask.Status = subTaskDto.Status;
                Database.SubTasks.Update(subTask);
                Database.SaveAsync();
            }
        }
    }
}
