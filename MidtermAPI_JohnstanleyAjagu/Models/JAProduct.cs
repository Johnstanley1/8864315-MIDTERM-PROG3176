using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MidtermAPI_JohnstanleyAjagu.Models
{
    public class JAProduct
    {
        public int Id { get; set; }

        [Required]
        [Range(1,25, ErrorMessage = "Name must be between 1 and 25 chars.")]
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}

