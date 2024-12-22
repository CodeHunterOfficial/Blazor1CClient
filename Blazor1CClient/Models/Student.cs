namespace Blazor1CClient.Models
{
    /// <summary>
    /// Модель, представляющая студента.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Уникальный идентификатор студента.
        /// </summary>
        public string Код { get; set; }

        /// <summary>
        /// Полное имя студента (фамилия, имя, отчество).
        /// </summary>
        public string ФИО { get; set; }

        /// <summary>
        /// Курс, на котором обучается студент.
        /// </summary>
        public int Курс { get; set; }

        /// <summary>
        /// Дата рождения студента.
        /// </summary>
        public DateTime ДатаРождения { get; set; }
    }
}
