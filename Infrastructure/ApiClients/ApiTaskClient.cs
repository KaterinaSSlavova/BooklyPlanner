using System.Text;
using System.Text.Json;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.ApiClients
{
    public class ApiTaskClient: IApiTaskClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ApiTaskClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("TaskInternalApi");
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ReadingTask?> GetTaskById(int taskId)
        {
            var response = await _client.GetAsync($"api/tasks/by-id/{taskId}");
            if(!response.IsSuccessStatusCode) return null;  

            string json = await response.Content.ReadAsStringAsync();   
            return JsonSerializer.Deserialize<ReadingTask>(json, _options);   
        }

        public async Task<List<ReadingTask>?> LoadUserTasks(int userId)
        {
            var response = await _client.GetAsync($"api/tasks/by-user/{userId}");
            if (!response.IsSuccessStatusCode) return new List<ReadingTask>();

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ReadingTask>>(json, _options) ?? new List<ReadingTask>();
        }

        public async Task<bool> CreateTask(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/create-task", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> MarkAsComplete(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/mark-as-complete", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ArchiveTask(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/archive-task", content);
            return response.IsSuccessStatusCode;
        }
    }
}
