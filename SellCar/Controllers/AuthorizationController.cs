//using Microsoft.AspNetCore.Mvc;
//using SellCar.Helpers;
//using SellCar.Models;

//namespace SellCar.Controllers
//{
//    public class AuthorizationController : ControllerBase
//    {
//        public static List<Clients> admins = SeedData.Seed();
//        [HttpGet("Authorization")]
//        public IActionResult Authorization(string loggin, string password)
//        {
//            var admin = admins.FirstOrDefault(x => x.Loggin == loggin && x.Password == password);
//            if (admin != null)
//            {
//                admin.Activate = true;
//                return Ok();
//            }

//            else
//                return BadRequest("Логин или пароль не верен");
//        }

//        [HttpPost("Registration")]
//        public IActionResult Registration(string loggin, string password)
//        {
//            var result = admins.FirstOrDefault(x => x.Loggin == loggin);

//            if (result != null)
//            {
//                return BadRequest("Такой логин уже существует");
//            }
//            else
//            {
//                Clients admin1 = new Clients
//                {
//                    Loggin = loggin,
//                    Password = password,
//                    Activate = false,
//                };
//                admins.Add(admin1);
//                return Ok(admin1);
//            }
//        }
//    }
//}
