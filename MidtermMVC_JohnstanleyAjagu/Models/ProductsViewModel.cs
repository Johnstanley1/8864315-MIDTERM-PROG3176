using System.ComponentModel.DataAnnotations;

namespace CarRentalMVC.Models
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
