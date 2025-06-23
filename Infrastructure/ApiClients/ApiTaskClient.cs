using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.ApiClients
{
    public class ApiTaskClient : IApiTaskClient
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
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("API error when loading task by id.");

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ReadingTask>(json, _options);
        }

        public async Task<List<ReadingTask>?> LoadUserTasks(int userId)
        {

            var response = await _client.GetAsync($"api/tasks/by-user/{userId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new List<ReadingTask>();

            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("API error when loading user tasks.");

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ReadingTask>>(json, _options);
        }

        public async Task CreateTask(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/create-task", content);
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("Api error when creating the task.");
        }

        public async Task MarkAsComplete(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/mark-as-complete", content);
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("Api error when completing the task.");
        }

        public async Task ArchiveTask(ReadingTask task)
        {
            var json = JsonSerializer.Serialize(task, _options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/tasks/archive-task", content);
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("Api error when archiving the task.");
        }
    }
}
