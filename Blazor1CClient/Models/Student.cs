namespace Blazor1CClient.Models
{
    public class Student
    {
        public string Код { get; set; }
        public string ФИО { get; set; }
        public int Курс { get; set; }  // Изменено на int
        public DateTime ДатаРождения { get; set; }
    }
}
