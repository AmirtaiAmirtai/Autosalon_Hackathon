using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using харкатон.Controllers.models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using харкатон.Helpers;

namespace харкатон.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private static decimal userBalance = 0;

        [HttpPost("setbalance")]
        public IActionResult SetBalance([FromBody] decimal balance)
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                userBalance = balance;
                return Ok($"User balance set to: {userBalance}");
            }
            return BadRequest("Вы не авторизованы");
        }

        [HttpPost("purchase/{Id}")]
        public IActionResult PurchaseCar(int Id)
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                var car = InfoHelper.cars.FirstOrDefault(t => t.Id == Id);
                if (car == null)
                {
                    return NotFound("Car not found");
                }

                if (userBalance < car.Price)
                {
                    return BadRequest("Insufficient funds to purchase the car");
                }

                userBalance -= car.Price;
                InfoHelper.cars.Remove(car);

                return Ok($"Car '{car.Name}' purchased. Remaining balance: {userBalance}");
            }
            return BadRequest("Вы не авторизованы");
        }
        [HttpGet("getbalance")]
        public IActionResult GetUserBalance()
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                return Ok($"Current balance: {userBalance}");
            }
            return BadRequest("Вы не авторизованы");
        }

        [HttpGet("see-cars")]
        public IActionResult GetCars()
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                return Ok(InfoHelper.cars);
            }
            return BadRequest("Вы не авторизованы");
            }

        [HttpGet("see-car-by/{id}")]
        public IActionResult GetCar(int id)
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
                if (car == null)
                    return NotFound();

                return Ok(car);
            }
            return BadRequest("Вы не авторизованы");
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar([FromBody] Car newCar)
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                if (newCar == null)
                {
                    return BadRequest("Invalid toy data");
                }

                newCar.Id = InfoHelper.cars.Count + 1;
                InfoHelper.cars.Add(newCar);
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
            }
            return BadRequest("Вы не авторизованы");
        }

        [HttpDelete("Delete-Car-By/{id}")]
        public IActionResult DeleteCar(int id)
        {
            if (AuthorizationController.admins.FirstOrDefault(x => x.Activate == true) != null)
            {
                var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
                if (car == null)
                    return NotFound();

                InfoHelper.cars.Remove(car);
                return NoContent();
            }
            return BadRequest("Вы не авторизованы");
        }
    }
}

