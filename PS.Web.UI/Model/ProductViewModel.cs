using PS.Entity;

namespace PS.Web.UI.Model
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();

        public List<Category> Categories { get; set; } = new();

        public ProductViewModel() {  }   
        
    }
}
