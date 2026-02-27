using API_Shashin11.Context;
using API_Shashin11.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API_Shashin11.Controllers
{
    [Route("api/UsersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class UsersController : Controller
    {
        ///<summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для авторизации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно авторизован</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("SignIn")]
        [HttpPost]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult SignIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);
            try
            {
                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == Password).First();
                return Json(User);
            } catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод предназначен для регистрации нового пользователя</returns>
        /// <response code="200">Пользователь успешно зарегистрирован</response>
        /// <response code="400">Логин или пароль не указаны</response>
        /// <response code="409">Пользователь с таким логином уже существует</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("RegIn")]
        [HttpPost]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password)
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                return BadRequest("Логин и пароль обязательны для заполнения"); // 400

            try
            {
                using (var context = new UsersContext())
                {
                    var existingUser = context.Users.FirstOrDefault(x => x.Login == Login);
                    if (existingUser != null)
                        return Conflict("Пользователь с таким логином уже существует");
                    var newUser = new Users
                    {
                        Login = Login,
                        Password = Password
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    return Ok(new
                    {
                        newUser.Login,
                        newUser.Password
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации: {ex.Message}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
