using System;
using ToDoListEnhanced.DAL.Entities;

namespace ToDoListEnhanced.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Project> Projects { get; }
        IRepository<SubTask> SubTasks { get; }
        void SaveAsync();
    }
}
