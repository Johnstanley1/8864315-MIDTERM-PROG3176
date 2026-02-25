using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidtermAPI_JohnstanleyAjagu.Models;

namespace MidtermAPI_JohnstanleyAjagu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JAProductController : ControllerBase
    {
        private readonly Dictionary<string, int> _usageCounts;
        public JAProductController(Dictionary<string, int> usageCounts) 
        {
            _usageCounts = usageCounts;
        }


        [HttpGet]
        public IActionResult Usage()
        {
            var key = Request.Headers["X-Api-Key"].ToString();

            if (!_usageCounts.ContainsKey(key))
                _usageCounts[key] = 0;

            _usageCounts[key]++;

            return Ok(new
            {
                clientId = key,
                usageCount = _usageCounts[key]
            });
        }


        [HttpGet]
        public ActionResult<IEnumerable<JAProduct>> Get()
        {
            var products = new List<JAProduct>
            {
                new JAProduct { Id = 1, Name = "Nissan Amada", Quantity = 10 },
                new JAProduct { Id = 2, Name = "Toyota Corrolla", Quantity = 20 },
                new JAProduct { Id = 3, Name = "Honda Civic", Quantity = 30 }
            };
            return Ok(products);
        }


        [HttpPost]
        public ActionResult<JAProduct> Post([FromBody] JAProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = "InvalidProduct",
                    message = "Name cannot be empty"
                });
            }

            return CreatedAtAction(nameof(Get), new { id = product.Id }, new
            {
                message = "Product created",
                product = product
            });
        }
    }
}
