using MidtermAPI_JohnstanleyAjagu.Models;

namespace MidtermAPI_JohnstanleyAjagu.Services
{
    public interface JAProductService
    {
        List<JAProduct> GetProducts();
        JAProduct PostProduct(JAProduct product);
    }
}
