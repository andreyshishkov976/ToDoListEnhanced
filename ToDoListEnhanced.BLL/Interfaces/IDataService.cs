using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListEnhanced.BLL.Interfaces
{
    public interface IDataService<T> where T : class
    {
        Task<ICollection<T>> Get(Guid? id, string token);
        Task Create(T Dto, string token);
        Task Update(T Dto, string token);
        Task Delete(T Dto, string token);
    }
}
