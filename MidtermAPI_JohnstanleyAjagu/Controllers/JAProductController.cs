using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidtermAPI_JohnstanleyAjagu.Models;
using MidtermAPI_JohnstanleyAjagu.Services;

namespace MidtermAPI_JohnstanleyAjagu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JAProductController : ControllerBase
    {
        private readonly Dictionary<string, int> _usageCounts;
        private readonly JAProductService _jAProductService;
        public JAProductController(JAProductService jAProductService, Dictionary<string, int> usageCounts)
        {
            _jAProductService = jAProductService;
            _usageCounts = usageCounts;
        }


        [HttpGet("usage")]
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
            return _jAProductService.GetProducts();
        }


        [HttpGet("{id}")]
        public ActionResult<JAProduct> GetById(int id)
        {
            var product = _jAProductService.GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }


        [HttpPost]
        public ActionResult Post([FromBody] JAProduct product)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    error = "InvalidProduct",
                    message = string.Join("; ", errors)
                });
            }

            var created = _jAProductService.PostProduct(product);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new
            {
                message = "Product created",
                Product = created
            });
        }
    }
}
