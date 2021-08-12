using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;

namespace ToDoListEnhanced.BLL.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserDTO userDto);
        Task<Dictionary<string, string>> AuthorizeUser(string login, string password);
    }
}
