using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListEnhanced.ApiBLL.DTO;

namespace ToDoListEnhanced.ApiBLL.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDTO>> Get();
        Task<ICollection<UserDTO>> Get(string login);
        void Create(UserDTO userDto);
        void Update(UserDTO userDto);
        void Delete(Guid id);
        Task<ClaimsIdentity> Authorize(string login, string password);
    }
}
