using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListEnhanced.DAL.EF;
using ToDoListEnhanced.DAL.Entities;
using ToDoListEnhanced.DAL.Interfaces;

namespace ToDoListEnhanced.DAL.Repositories
{
    class ProjectRepository : IRepository<Project>
    {
        private ToDoListContext _context;

        public ProjectRepository(ToDoListContext context)
        {
            this._context = context;
        }

        public void Create(Project item)
        {
            //_context.Entry(item).State = EntityState.Added;
            _context.Projects.Add(item);
        }

        public void Delete(Guid id)
        {
            Project item = _context.Projects.Find(id);
            if (item != null)
                //_context.Entry(item).State = EntityState.Deleted;
                _context.Projects.Remove(item);
        }

        public async Task<ICollection<Project>> Find(Func<Project, bool> predicate)
        {
            return await Task.Run(() => { return _context.Projects.Where(predicate).ToList(); });
        }

        public Project Get(Guid id)
        {
            return _context.Projects.Find(id);
        }

        public async Task<ICollection<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public void Update(Project item)
        {
            _context.Projects.Update(item);
            //_context.Entry(item).State = EntityState.Modified;
        }
    }
}
