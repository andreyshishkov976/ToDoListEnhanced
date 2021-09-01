using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListEnhanced.ClientBLL.DTO;

namespace ToDoListEnhanced.ClientBLL.Interfaces
{
    public interface IUserWebService
    {
        Task RegisterUser(UserDTO userDto);
        Task<Dictionary<string, string>> AuthorizeUser(string login, string password);
    }
}
