using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using харкатон.Controllers.models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace харкатон.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private static List<Car> cars = new List<Car>
    {
        new Car { Id = 1, Name = "SleekSport ZR5000", Price = 20.99m, Description = "The SleekSport ZR5000 is a high-performance sports car designed for " +
            "enthusiasts who crave speed and precision. With its aerodynamic curves, low profile, and a powerful V8 engine under the hood, the ZR5000 can" +
            " accelerate from 0 to 60 mph in just 3.5 seconds. The interior features a futuristic cockpit with cutting-edge technology, including a touchscreen" +
            " infotainment system and advanced driver-assistance features. Whether on the track or the open road, the SleekSport ZR5000 delivers an" +
            " exhilarating driving experience." },

        new Car { Id = 2, Name = "Urban Voyager XL", Price = 34.99m, Description = "The Urban Voyager XL is a versatile and spacious SUV" +
            " designed for urban adventurers and families alike. Its robust exterior design not only exudes confidence but also provides" +
            " ample cargo space and comfortable seating for up to seven passengers. Equipped with the latest safety features and a" +
            " fuel-efficient hybrid engine, the Urban Voyager XL seamlessly combines performance and practicality. Inside, the cabin" +
            " boasts luxury materials, a panoramic sunroof, and a user-friendly entertainment system, making it the perfect companion" +
            " for city exploration and family road trips."},

        new Car { Id = 3, Name = "EcoDrive E1", Price = 49.99m, Description = "The EcoDrive E1 is an eco-friendly compact car that" +
            " prioritizes sustainability without compromising on style. With its all-electric powertrain, the E1 produces zero emissions," +
            " making it an ideal choice for environmentally conscious drivers. Despite its compact size, the interior is" +
            " surprisingly spacious, featuring modern amenities and a minimalist design. The EcoDrive E1 is not only budget-friendly" +
            " but also boasts an impressive range on a single charge, making it a practical and green solution for daily commuting in" +
            " urban environments." }
    };



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
            var car = cars.FirstOrDefault(t => t.Id == Id);
            if (car == null)
            {
                return NotFound("Car not found");
            }

            if (userBalance < car.Price)
            {
                return BadRequest("Insufficient funds to purchase the car");
            }

            userBalance -= car.Price;
            cars.Remove(car);

            return Ok($"Car '{car.Name}' purchased. Remaining balance: {userBalance}");
        }
        [HttpGet("getbalance")]
        public IActionResult GetUserBalance()
        {
            return Ok($"Current balance: {userBalance}");
        }

        [HttpGet("see cars")]
        public IActionResult GetCars()
        {
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost("add")]
        public IActionResult AddCar([FromBody] Car newCar)
        {
            if (newCar == null)
            {
                return BadRequest("Invalid toy data");
            }

            newCar.Id = cars.Count + 1;
            cars.Add(newCar);
            return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
                return NotFound();

            cars.Remove(car);
            return NoContent();
        }
    }
}

