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
    class UserRepository : IRepository<User>
    {
        private ToDoListContext _context;

        public UserRepository(ToDoListContext context)
        {
            this._context = context;
        }

        public void Create(User item)
        {
            _context.Entry(item).State = EntityState.Added;
            //_context.Users.Add(item);
        }

        public void Delete(Guid id)
        {
            User item = _context.Users.Find(id);
            if (item != null)
                _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task<ICollection<User>> Find(Func<User, bool> predicate)
        {
            return await Task.Run(() => { return _context.Users.Where(predicate).ToList(); });
        }

        public User Get(Guid id)
        {
            return _context.Users.Find(id);
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
