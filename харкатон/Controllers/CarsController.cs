using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using харкатон.Controllers.models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using харкатон.Helpers;
using харкатон.Models;
using Microsoft.AspNetCore.Authorization;

namespace харкатон.Controllers
{

    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
    private static decimal userBalance = 0;



        //установление баланса
        [HttpPost("setbalance")]
        public async Task<IActionResult> SetBalance([FromBody] decimal balance)
        {
            await Task.Run(() =>
            {
                userBalance = balance;
            });

            return Ok($"Баланс пользователя установлен: {userBalance}");
        } 



        //покупка машины. удаляет машину из списка после покупки.
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
                InfoHelper.purchasedCars.Add(car);
            });

            return Ok($"Автомобиль '{car.Name}' куплен. Остаток на балансе: {userBalance}");
        }



        //Возврат машины
        [HttpPost("Refund/{Id}")] 
        public async Task<IActionResult> RefundCar(int Id)
        {
            var car = InfoHelper.purchasedCars.FirstOrDefault(t => t.Id == Id);
            if (car == null)
            {
                return NotFound("Автомобиль не найден");
            }

            await Task.Run(() =>
            {
                userBalance += car.Price;
                InfoHelper.cars.Add(car);
                InfoHelper.purchasedCars.Remove(car);
            });

            return Ok($"Возврат автомобиля'{car.Name}' осуществлён. Остаток на балансе: {userBalance}");
        }



        //Просмотр баланса
        [HttpGet("getbalance")]
        public async Task<IActionResult> GetUserBalance()
        {
            return Ok($"Текущий баланс: {userBalance}");
        }


        //Просмотр всех машин
        [HttpGet("see-cars")]
        public async Task<IActionResult> GetCars()
        {
            return Ok(InfoHelper.cars);
        }



        //Просмотр купленных машин
        [HttpGet("see-purchased-cars")]
        public async Task<IActionResult> GetPurchasedCars()
        {
            return Ok(InfoHelper.purchasedCars);
        }



        //Просмотр конкретной машины по ID
        [HttpGet("see-car-by/{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }


       
    }


}


