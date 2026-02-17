using System;

namespace API_Shashin1.Model
{
    /// <summary>
    /// Задачи
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Код
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Дата выполнения
        /// </summary>
        public DateTime DateExecute { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Статус выполнения
        /// </summary>
        public bool Done { get; set; }
    }
}
