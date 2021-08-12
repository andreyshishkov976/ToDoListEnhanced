using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Interfaces;

namespace ToDoListEnhanced.BLL.Services
{
    public class ProjectService : IDataService<ProjectDTO>
    {
        public ProjectService()
        { }

        public async Task<ICollection<ProjectDTO>> Get(Guid? id, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                var response = await client.GetAsync($@"https://localhost:44338/api/Projects/Get/{id}");
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ICollection<ProjectDTO>>(result);
            }
        }

        public async Task Create(ProjectDTO projectDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                await client.PostAsJsonAsync($@"https://localhost:44338/api/Projects/Create", projectDto);
            }
        }

        public async Task Update(ProjectDTO projectDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                await client.PutAsJsonAsync($@"https://localhost:44338/api/Projects/Update", projectDto);
            }
        }

        public async Task Delete(ProjectDTO projectDto, string accessToken)
        {
            using (var client = HttpClientService.CreateClient(accessToken))
            {
                await client.DeleteAsync($@"https://localhost:44338/api/Projects/Delete/{projectDto.Id}");
            }
        }
    }
}
