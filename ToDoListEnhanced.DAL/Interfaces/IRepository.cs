using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListEnhanced.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        T Get(Guid id);
        Task<ICollection<T>> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
