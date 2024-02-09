using Microsoft.AspNetCore.Mvc;
using харкатон.Controllers.models;
using харкатон.Helpers;

namespace харкатон.Controllers 
{
    public class AdminkaController : Controller
    {
        // данные для получения админки
        private const string SystemUsername = "admin";
        private const string SystemPassword = "admin123";

        public static UserCredentials user = new UserCredentials
        {
            Username = SystemUsername,
            Password = SystemPassword
        };


        [HttpPost("Admin/Login")]

        public ActionResult LoginAsAdmin(string Username, string Password)
        {
            // Проверяем совпадение введенных данных с системными
            if (Username == SystemUsername && Password == SystemPassword)
            {
                user.IsActive = true;
                return Ok("Вход разрешен. Доступ предоставлен.");
            }
            else
                return BadRequest("Ошибка входа. Неверное имя пользователя или пароль.");
        }


        public class UserCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
        }


        //Добавление новой машины в список машин в наличии. Функция для администратора
        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] Car newCar)
        {
            if (user.IsActive == true)
            {
                if (newCar == null)
                {
                    return BadRequest("Неверные данные для автомобиля");
                }

                newCar.Id = InfoHelper.cars.Count + 1;

                await Task.Run(() =>
                {
                    InfoHelper.cars.Add(newCar);
                });

                return CreatedAtAction("Добавлена машина: \n" + nameof(GetCar), new { id = newCar.Id }, newCar);
            }
            else
            {
                return BadRequest("Недостаточно прав для осуществления данной команды.");
            }
        }

        [HttpGet("{id}", Name = "GetCar")]
        [ApiExplorerSettings(IgnoreApi = true)] 
        public IActionResult GetCar(int id)

        {
            var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }




        //Удаление машины из списка машин в наличии. Функция для администратора
        [HttpDelete("Delete-Car-By/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
            if (user.IsActive == true)
            {
                if (car == null)
                    return BadRequest("Машины с таким ID не существует.");

                await Task.Run(() =>
                {
                    InfoHelper.cars.Remove(car);
                    OtherLists.DeletedCars.Add(car);
                });

                return Ok($"Удалена машина {car.Name}.");
            }
            else
                return BadRequest("Недостаточно прав для осуществления данной команды.");
        }

        [HttpGet("See Deleted Cars")]
        public IActionResult GetDeletedCars()

        {
            var DeletedCars = OtherLists.DeletedCars;
            
            return Ok(DeletedCars);
        }

        [HttpPost("Return-Deleted-Car-By/{id}")]
        public async Task<IActionResult> ReturnDeletedCar(int id)
        {
            var car = OtherLists.DeletedCars.FirstOrDefault(t => t.Id == id);
            if (user.IsActive == true)
            {
                if (car == null)
                    return BadRequest("Удаленной машины с таким ID не существует.");

                await Task.Run(() =>
                {
                    OtherLists.DeletedCars.Remove(car);
                    InfoHelper.cars.Add(car);
                });

                return Ok($"машина {car.Name} возвращена в список машин в наличии.");
            }
            else
                return BadRequest("Недостаточно прав для осуществления данной команды.");
        }
    }
    }

