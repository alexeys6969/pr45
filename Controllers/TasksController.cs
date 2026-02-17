using API_Shashin1.Context;
using API_Shashin1.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Shashin1.Controllers
{
    [Route("api/TasksController")]
    public class TasksController : Controller
    {
        ///<summary>
        ///Получение списка задач
        /// </summary>
        /// <remarks>Данный метод получает список задач, находящийся в базе данных</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Task>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Task> Tasks = new TasksContext().Tasks;
                return Json(Tasks);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        ///<summary>
        ///Получение задачи
        /// </summary>
        /// <remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Task), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Task Tasks = new TasksContext().Tasks.Where(x => x.Id == Id).First(); ;
                return Json(Tasks);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
