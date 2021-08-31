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
    class SubTaskRepository : IRepository<SubTask>
    {
        private ToDoListContext _context;

        public SubTaskRepository(ToDoListContext context)
        {
            this._context = context;
        }

        public void Create(SubTask item)
        {
            _context.SubTasks.Add(item);
            //_context.Entry(item).State = EntityState.Added;
        }

        public void Delete(Guid id)
        {
            SubTask item = _context.SubTasks.Find(id);
            if (item != null)
                //_context.Entry(item).State = EntityState.Deleted;
                _context.SubTasks.Remove(item);
        }

        public async Task<ICollection<SubTask>> Find(Func<SubTask, bool> predicate)
        {
            return await Task.Run(() => { return _context.SubTasks.Where(predicate).ToList(); });
        }

        public SubTask Get(Guid id)
        {
            return _context.SubTasks.Find(id);
        }

        public async Task<ICollection<SubTask>> GetAll()
        {
            return await _context.SubTasks.ToListAsync();
        }

        public void Update(SubTask item)
        {
            _context.SubTasks.Update(item);
            //_context.Entry(item).State = EntityState.Modified;
        }
    }
}
