using Blazor1CClient.Models;

namespace Blazor1CClient.Services
{
    /// <summary>
    /// Интерфейс для сервиса работы с данными студентов.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Получение списка всех студентов.
        /// </summary>
        /// <returns>Список студентов.</returns>
        Task<List<Student>> GetStudentsAsync();

        /// <summary>
        /// Добавление нового студента.
        /// </summary>
        /// <param name="student">Данные студента для добавления.</param>
        /// <returns>Статус выполнения операции.</returns>
        Task<bool> AddStudentAsync(Student student);

        /// <summary>
        /// Обновление информации о студенте.
        /// </summary>
        /// <param name="id">Идентификатор студента, чьи данные обновляются.</param>
        /// <param name="student">Новые данные студента.</param>
        /// <returns>Статус выполнения операции.</returns>
        Task<bool> UpdateStudentAsync(string id, Student student);

        /// <summary>
        /// Удаление студента.
        /// </summary>
        /// <param name="id">Идентификатор студента, которого нужно удалить.</param>
        /// <returns>Статус выполнения операции.</returns>
        Task<bool> DeleteStudentAsync(string id);

        /// <summary>
        /// Получение студента по его коду.
        /// </summary>
        /// <param name="code">Код студента для поиска.</param>
        /// <returns>Студент с указанным кодом или null, если не найден.</returns>
        Task<Student?> GetStudentByCodeAsync(string code);
    }
}
