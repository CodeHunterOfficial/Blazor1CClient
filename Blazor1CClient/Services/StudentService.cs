using Blazor1CClient.Models;
using System.Text;
using System.Text.Json;

namespace Blazor1CClient.Services
{
    public class StudentService:IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StudentService> _logger;

        public StudentService(HttpClient httpClient, ILogger<StudentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var students = await _httpClient.GetFromJsonAsync<List<Student>>("getListOfStudents");
                if (students == null)
                {
                    _logger.LogWarning("Не удалось получить список студентов.");
                }
                return students ?? new List<Student>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка студентов");
                return new List<Student>();
            }
        }
      
        public async Task<bool> AddStudentAsync(Student student)
        {
            if (student == null)
            {
                _logger.LogWarning("Попытка добавить пустого студента.");
                return false;
            }

            try
            {
                var response = await _httpClient.PostAsync("postStudent",
                    new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Студент добавлен успешно.");
                    return true;
                }

                _logger.LogWarning("Ошибка при добавлении студента. Статус: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении студента");
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(string Код, Student student)
        {
            if (string.IsNullOrEmpty(Код) || student == null)
            {
                _logger.LogWarning("Попытка обновить студента с пустыми данными.");
                return false;
            }

            try
            {
                var response = await _httpClient.PutAsync($"updateStudent/{Код}",
                    new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Данные студента обновлены успешно.");
                    return true;
                }

                _logger.LogWarning("Ошибка при обновлении студента. Статус: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении студента");
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Попытка удалить студента с пустым id.");
                return false;
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"deleteStudent/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Студент удален успешно.");
                    return true;
                }

                _logger.LogWarning("Ошибка при удалении студента. Статус: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении студента");
                return false;
            }
        }

        public async Task<Student?> GetStudentByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                _logger.LogWarning("Попытка получить студента с пустым кодом.");
                return null;
            }

            try
            {
                var students = await GetStudentsAsync(); // Получаем весь список студентов
                var student = students.FirstOrDefault(s => s.Код == code);  // Ищем студента по коду

                if (student == null)
                {
                    _logger.LogWarning("Студент с кодом {Code} не найден.", code);
                }
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении студента по коду");
                return null;
            }
        }
    }
}