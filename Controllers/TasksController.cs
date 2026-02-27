using API_Shashin11.Context;
using API_Shashin11.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace API_Shashin11.Controllers
{
    [Route("api/TasksController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TasksController : Controller
    {
        ///<summary>
        ///Получение списка задач
        /// </summary>
        /// <remarks>Данный метод получает список задач в БД</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Tasks> Tasks = new TasksContext().Tasks;
                return Json(Tasks);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        ///Получение задачи
        /// </summary>
        /// <remarks>Данный метод получает задачу из БД</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item/{Id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Tasks Task = new TasksContext().Tasks.Where(x => x.Id == Id).First();
                return Json(Task);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        ///Получение задачи по имени
        /// </summary>
        /// <remarks>Данный метод получает задачу из БД</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item/search={Name}")]
        [HttpGet]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(string Name)
        {
            try
            {
                Tasks Task = new TasksContext().Tasks.Where(x => x.Name == Name).First();
                return Json(Task);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        /// Метод добавления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод добавляет задачу в базу данных</remarks>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm]Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                tasksContext.Tasks.Add(task);
                tasksContext.SaveChanges();
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        /// Метод обновления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод обновляет задачу в базу данных</remarks>
        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                var existingTask = tasksContext.Tasks.Find(task.Id);
                if (existingTask == null)
                    return NotFound();
                existingTask.Name = task.Name;
                existingTask.Priority = task.Priority;
                existingTask.DateExecute = task.DateExecute;
                existingTask.Comment = task.Comment;
                existingTask.Done = task.Done;
                tasksContext.SaveChanges();
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        /// Метод удаления задачи
        /// </summary>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод удаляет задачу из базы данных</remarks>
        [Route("Delete")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (var context = new TasksContext())
                {
                    // Находим задачу
                    var task = context.Tasks.FirstOrDefault(x => x.Id == Id);

                    // Если задача не найдена
                    if (task == null)
                        return NotFound($"Задача с ID {Id} не найдена");

                    // Удаляем задачу
                    context.Tasks.Remove(task);
                    context.SaveChanges();

                    return Ok($"Задача с ID {Id} успешно удалена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        ///<summary>
        /// Метод удаления записей из таблицы
        /// </summary>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод удаляет записи из таблицы</remarks>
        [Route("DeleteFrom1111")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteFrom1111()
        {
            try
            {
                TasksContext context = new TasksContext();
                var allTasks = context.Tasks.ToList();
                context.RemoveRange(allTasks);
                context.SaveChanges();
                return Ok($"Удалено {allTasks.Count} записей");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
