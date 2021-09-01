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
    public class ProjectService : IDataService<ProjectDTO>
    {
        IUnitOfWork Database;

        public ProjectService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(ProjectDTO projectDto)
        {
            Project project = new Project
            {
                Id = projectDto.Id,
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                Status = projectDto.Status,
                UserId = projectDto.UserId,
                User = Database.Users.Get(projectDto.UserId),
                SubTasks = new List<SubTask>()
            };
            Database.Projects.Create(project);
            Database.SaveAsync();
        }

        public void Delete(Guid id)
        {
            Database.Projects.Delete(id);
            Database.SaveAsync();
        }

        public async Task<ICollection<ProjectDTO>> Get(Guid? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<ICollection<Project>, List<ProjectDTO>>(await Database.Projects.Find(item => item.UserId == id));
        }

        public void Update(ProjectDTO projectDto)
        {
            Project project = Database.Projects.Get(projectDto.Id);
            if (project != null)
            {
                project.ProjectName = projectDto.ProjectName;
                project.Description = projectDto.Description;
                project.Status = projectDto.Status;
                Database.Projects.Update(project);
                Database.SaveAsync();
            }
        }
    }
}
