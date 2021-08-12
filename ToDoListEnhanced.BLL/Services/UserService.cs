using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Infrastructure;
using ToDoListEnhanced.BLL.Interfaces;

namespace ToDoListEnhanced.BLL.Services
{
    public class UserService : IUserService
    {
        public UserService()
        { }

        public async Task RegisterUser(UserDTO userDto)
        {
            using (var client = HttpClientService.CreateClient())
            {
                var response = await client.GetAsync($@"https://localhost:44338/api/Users/Get/{userDto.Login}");
                var result = await response.Content.ReadAsStringAsync();
                if (JsonConvert.DeserializeObject<List<UserDTO>>(result).Count != 0)
                    throw new AuthentificationException("Пользователь с данным логином уже существует.", userDto.Login, userDto.PasswordHash);
                await client.PostAsJsonAsync($@"https://localhost:44338/api/Users/Register", userDto);
            }
        }


        public async Task<Dictionary<string, string>> AuthorizeUser(string login, string password)
        {
            Dictionary<string, string> authUser;
            using (var client = HttpClientService.CreateClient())
            {
                var response = await client.PostAsync($@"https://localhost:44338/api/Users/Authorize/{login}&{password}", null);
                var result = await response.Content.ReadAsStringAsync();
                authUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                if (!response.IsSuccessStatusCode)
                    throw new AuthentificationException(authUser["errorText"], login, password);
                else
                    return authUser;
            }
        }
    }
}
