using Microsoft.AspNetCore.Mvc;
using харкатон.Controllers.models;
using харкатон.Helpers;
namespace харкатон.Controllers
{
    public class PromocodeController : Controller
    {

        // Система промокодов - ввод одного из трех промокодов дает разные скидки на все машины в зависимости от введенного промокода 
        public string SystemPromocode = "ILOVECARS";
        public string SystemPromocode2 = "ISOLDMYKIDS";
        public string SystemPromocode1 = "IAMPOOR";
        private static bool IsPromoCodeUsed;

        [HttpPost("use-promocode")]
        public ActionResult UsePromocode(string Promocode)
        {
            var promos = OtherLists.promocodes;
            // Проверяем совпадение введенных данных с системными
            var cars = InfoHelper.cars;
            if (IsPromoCodeUsed == true)
            {
                return BadRequest("Промокод уже применен.");
            }

            if (Promocode == SystemPromocode)
            {

                foreach (var car in cars)
                {
                    car.Price *= 0.9m;
                }
                IsPromoCodeUsed = true;
                promos.Add(SystemPromocode);
                return Ok("Промокод успешно применен. Действует скидка на все машины в размере 10%.");
            }


            if (Promocode == SystemPromocode1)
            {
                foreach (var car in cars)
                {
                    
                    car.Price *= 0.8m;
                }
                IsPromoCodeUsed = true;
                promos.Add(SystemPromocode1);
                return Ok("Промокод успешно применен. Действует скидка на все машины в размере 20%.");
            }


            if (Promocode == SystemPromocode2)
            {
                foreach (var car in cars)
                {
                    car.Price *= 0.7m;
                }

                IsPromoCodeUsed = true;
                promos.Add(SystemPromocode2);
                return Ok("Промокод успешно применен. Действует скидка на все машины в размере 30%.");
            }

            else
            {
                return BadRequest("Ошибка применения промокода.");
            }
        }


        [HttpDelete("delete-promocode")] //удаление промокода
        public async Task<IActionResult> DeletePromo(string promo)
        {
            var promocode = OtherLists.promocodes;
            var cars = InfoHelper.cars;
            if (promo == SystemPromocode)
            {
                
                await Task.Run(() =>
                {
                    OtherLists.promocodes.Remove(promo);
                    IsPromoCodeUsed = false;
                    foreach (var car in cars)
                    {
                        car.Price *= 1.1m;
                    }
                });

                return NoContent();
            }

            if (promo == SystemPromocode1)
            {

                await Task.Run(() =>
                {
                    OtherLists.promocodes.Remove(promo);
                    IsPromoCodeUsed = false;
                    foreach (var car in cars)
                    {
                        car.Price *= 1.2m;
                    }
                });

                return NoContent();
            }

            if (promo == SystemPromocode2)
            {

                await Task.Run(() =>
                {
                    OtherLists.promocodes.Remove(promo);
                    IsPromoCodeUsed = false;
                    foreach (var car in cars)
                    {
                        car.Price *= 1.3m;
                    }
                });

                return NoContent();
            }
            if (promo == null)
                    return BadRequest("Такого промокода не существует.");

            else
                return BadRequest("Такого промокода не существует.");
        }
    }
}
