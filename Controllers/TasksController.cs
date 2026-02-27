using API_Shashin11.Context;
using API_Shashin11.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
    }
}
