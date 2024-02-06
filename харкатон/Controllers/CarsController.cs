using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using харкатон.Controllers.models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using харкатон.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace харкатон.Controllers
{
  
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {

        private static decimal userBalance = 0;

        [HttpPost("setbalance")]
        public async Task<IActionResult> SetBalance([FromBody] decimal balance)
        {
            await Task.Run(() => 
            {
                userBalance = balance;
            });

            return Ok($"Баланс пользователя установлен: {userBalance}");
        }

        [HttpPost("purchase/{Id}")]
        public async Task<IActionResult> PurchaseCar(int Id)
        {
            var car = InfoHelper.cars.FirstOrDefault(t => t.Id == Id);
            if (car == null)
            {
                return NotFound("Автомобиль не найден");
            }

            if (userBalance < car.Price)
            {
                return BadRequest("Недостаточно средств для покупки автомобиля");
            }

            await Task.Run(() => 
            {
                userBalance -= car.Price;
                InfoHelper.cars.Remove(car);
            });

            return Ok($"Автомобиль '{car.Name}' куплен. Остаток на балансе: {userBalance}");
        }

        [HttpGet("getbalance")]
        public async Task<IActionResult> GetUserBalance()
        {
            return Ok($"Текущий баланс: {userBalance}");
        }

        [HttpGet("see-cars")]
        public async Task<IActionResult> GetCars()
        {
            return Ok(InfoHelper.cars);
        }

        [HttpGet("see-car-by/{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }
        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] Car newCar)
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

            return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
        }
    }

    [HttpDelete("Delete-Car-By/{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
        if (car == null)
            return NotFound();

        await Task.Run(() => 
        {
            InfoHelper.cars.Remove(car);
        });

        return NoContent();
    }
}
}

