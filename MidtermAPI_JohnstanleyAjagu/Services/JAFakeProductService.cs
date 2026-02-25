using MidtermAPI_JohnstanleyAjagu.Models;

namespace MidtermAPI_JohnstanleyAjagu.Services
{
    public class JAFakeProductService : JAProductService
    {
        private readonly List<JAProduct> _products;

        public JAFakeProductService()
        {
            _products = new List<JAProduct>
            {
                new JAProduct { Id = 1, Name = "Nissan Armada", Quantity = 10 },
                new JAProduct { Id = 2, Name = "Toyota Corolla", Quantity = 20 },
                new JAProduct { Id = 3, Name = "Honda Civic", Quantity = 30 }
            };
        }

        public List<JAProduct> GetProducts()
        {
            return _products;
        }

        public JAProduct PostProduct(JAProduct product)
        {
            int newId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

            var newProduct = new JAProduct
            {
                Id = newId,
                Name = product.Name,
                Quantity = product.Quantity
            };

            _products.Add(newProduct);

            return newProduct;
        }
    }
}
