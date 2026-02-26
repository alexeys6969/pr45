using System;

namespace API_Shashin11.Model
{
    /// <summary>
    /// Задачи
    /// </summary>
    public class Tasks
    {
        /// <summary>
        /// Код
        /// </summary>
        public int Id {  get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime DateExecute {  get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        public bool Done { get; set; }
    }
}
