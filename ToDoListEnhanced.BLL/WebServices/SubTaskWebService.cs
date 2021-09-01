using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoListEnhanced.ClientBLL.DTO;
using ToDoListEnhanced.ClientBLL.Interfaces;

namespace ToDoListEnhanced.ClientBLL.WebServices
{
    public class SubTaskWebService : IDataWebService<SubTaskDTO>
    {
        public SubTaskWebService()
        { }

        public async Task<ICollection<SubTaskDTO>> Get(Guid? id, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                var response = await client.GetAsync($@"https://localhost:44338/api/SubTasks/Get/{id}");
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<SubTaskDTO>>(result);
            }
        }

        public async Task Create(SubTaskDTO subTaskDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                var response = await client.PostAsJsonAsync($@"https://localhost:44338/api/SubTasks/Create", subTaskDto);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
        }

        public async Task Update(SubTaskDTO subTaskDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                var response = await client.PutAsJsonAsync($@"https://localhost:44338/api/SubTasks/Update", subTaskDto);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
        }

        public async Task Delete(SubTaskDTO subTaskDTO, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                var response = await client.DeleteAsync($@"https://localhost:44338/api/SubTasks/Delete/{subTaskDTO.Id}");
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
        }
    }
}
