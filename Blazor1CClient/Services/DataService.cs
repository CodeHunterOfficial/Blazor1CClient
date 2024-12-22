using Blazor1CClient.Models;
using System.Text;
using System.Text.Json;
namespace Blazor1CClient.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Student>>("getListOfStudents");
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            var response = await _httpClient.PostAsync("postStudent",
                new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentAsync(string id, Student student)
        {
            var response = await _httpClient.PutAsync($"updateStudent/{id}",
                new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudentAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"deleteStudent/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}