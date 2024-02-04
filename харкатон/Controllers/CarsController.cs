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
        public IActionResult SetBalance([FromBody] decimal balance)
        {
            
                userBalance = balance;
                return Ok($"User balance set to: {userBalance}");
           
        }

        [HttpPost("purchase/{Id}")]
        public IActionResult PurchaseCar(int Id)
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
        [HttpGet("getbalance")]
        public IActionResult GetUserBalance()
        {
           
                return Ok($"Current balance: {userBalance}");
          
        }

        [HttpGet("see-cars")]
        public IActionResult GetCars()
        {
           
                return Ok(InfoHelper.cars);
           
            }

        [HttpGet("see-car-by/{id}")]
        public IActionResult GetCar(int id)
        {
                var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
                if (car == null)
                    return NotFound();

                return Ok(car);
          
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar([FromBody] Car newCar)
        {
           
                if (newCar == null)
                {
                    return BadRequest("Invalid toy data");
                }

                newCar.Id = InfoHelper.cars.Count + 1;
                InfoHelper.cars.Add(newCar);
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
          
        }

        [HttpDelete("Delete-Car-By/{id}")]
        public IActionResult DeleteCar(int id)
        {
            
                var car = InfoHelper.cars.FirstOrDefault(t => t.Id == id);
                if (car == null)
                    return NotFound();

                InfoHelper.cars.Remove(car);
                return NoContent();
           
        }
    }
}

