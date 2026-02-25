using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidtermAPI_JohnstanleyAjagu.Models;

namespace MidtermAPI_JohnstanleyAjagu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JAProductController : ControllerBase
    {
        public JAProductController() { }


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


    }
}
