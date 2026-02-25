using CarRentalMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalMVC.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MaintenanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Usage()
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");
            var result = await client.GetFromJsonAsync<object>("api/RepairHistory/usage");
            return View(result);
        }


        public async Task<IActionResult> Transfer(int fromId, int toId, decimal amount)
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");
            var response = await client.PostAsync(
            $"api/RepairHistory/transfer?fromId={fromId}&toId={toId}&amount={amount}",
            null);
            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Result = content;
            return View();
        }


        [HttpGet]
        public IActionResult History()
        {
            return View(new List<ProductsViewModel>());
        }


        [HttpPost]
        public async Task<IActionResult> History(int vehicleId)
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");

            var repairs = await client.GetFromJsonAsync<List<ProductsViewModel>>(
            $"api/maintenance/vehicles/{vehicleId}/repairs");

            return View(repairs ?? new List<ProductsViewModel>());
        }
    }
}
