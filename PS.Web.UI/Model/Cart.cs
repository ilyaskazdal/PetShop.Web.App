using PS.Entity;

namespace PS.Web.UI.Model
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public virtual void AddItem(Product product,int quantity)
        {
            var item = Items.Where(p=>p.Product.ProductId== product.ProductId).FirstOrDefault();
            if (item == null)
            {
                Items.Add(new CartItem() {  Product = product,Quantity = quantity});
            }
            else
            {
                item.Quantity += quantity;
            }

        }
        public virtual void RemoveItem(Product product)
        { 
            Items.RemoveAll(i=>i.Product.ProductId== product.ProductId);
        
        }
        public double CalculateTotal()
        {
            return Items.Sum(i=>i.Product.UnitPrice* i.Quantity);
        }
        public virtual void Clear()
        { 
            Items.Clear();
        
        }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }

    }
}
