using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Interfaces;

namespace ToDoListEnhanced.BLL.Services
{
    public class SubTaskService : IDataService<SubTaskDTO>
    {
        public SubTaskService()
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
                await client.PostAsJsonAsync($@"https://localhost:44338/api/SubTasks/Create", subTaskDto);
            }
        }

        public async Task Update(SubTaskDTO subTaskDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                await client.PutAsJsonAsync($@"https://localhost:44338/api/SubTasks/Update", subTaskDto);
            }
        }

        public async Task Delete(SubTaskDTO subTaskDTO, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                await client.DeleteAsync($@"https://localhost:44338/api/SubTasks/Delete/{subTaskDTO.Id}");
            }
        }
    }
}
