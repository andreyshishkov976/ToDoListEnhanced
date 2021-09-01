using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListEnhanced.ApiBLL.Interfaces
{
    public interface IDataService<T> where T : class
    {
        Task<ICollection<T>> Get(Guid? id);
        void Create(T Dto);
        void Update(T Dto);
        void Delete(Guid id);
    }
}
