//using Microsoft.AspNetCore.Mvc;
//using харкатон.Helpers;
//using харкатон.Models;

//namespace харкатон.Controllers
//{
//    public class UserController : Controller
//    {
//        private const string SystemUsername = "admin";
//        private const string SystemPassword = "admin123";

//        [HttpPost]
//        [Route("api/login")]
//        public ActionResult Login([FromBody] UserCredentials userCredentials)
//        {
//            // Проверяем совпадение введенных данных с системными
//            if (userCredentials.Username == SystemUsername && userCredentials.Password == SystemPassword)
//            {
//                return Ok("Вход разрешен. Доступ предоставлен.");
//            }
//            else
//            {
//                return BadRequest("Ошибка входа. Неверное имя пользователя или пароль.");
//            }
//        }
//    }

//    public class UserCredentials
//    {
//        public string Username { get; set; }
//        public string Password { get; set; }
//    }
//}
