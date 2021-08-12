using System;
using ToDoListEnhanced.DAL.EF;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;

namespace ToDoListEnhanced.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ToDoListContext _context;
        private UserRepository _userRepository;
        private ProjectRepository _projectRepository;
        private SubTaskRepository _subTaskRepository;

        public EFUnitOfWork(ToDoListContext dbContext)
        {
            _context = dbContext;
        }
        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_context);
                return _projectRepository;
            }
        }

        public IRepository<SubTask> SubTasks
        {
            get
            {
                if (_subTaskRepository == null)
                    _subTaskRepository = new SubTaskRepository(_context);
                return _subTaskRepository;
            }
        }

        public void SaveAsync()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
