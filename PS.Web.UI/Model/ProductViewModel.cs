using PS.Entity;
using System.ComponentModel.DataAnnotations;


namespace PS.Web.UI.Model
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();

        public List<Category> Categories { get; set; } = new();

        public User User { get; set; }

        
    }
}
