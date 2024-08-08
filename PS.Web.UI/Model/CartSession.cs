using PS.Entity;
using PS.Web.UI.Helpers;
using PS.Web.UI.Pages;
using System.Configuration;
using System.Text.Json.Serialization;

namespace PS.Web.UI.Model
{
    public class CartSession : Cart
    {
        //constructer yok veri çekemiyoruzmethod lazm servis conteinerı programda yazmak lzm methodla çekicez
        public static Cart GetCart(IServiceProvider service)
        {
            ISession? session = service.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            CartSession cart = session?.GetJson<CartSession>("Cart") ?? new CartSession(); //json helpersda helpers eklenmesi lzm
            cart.Session = session;
            return cart;
        }
        [JsonIgnore] //serilizationa gerek yok
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson("Cart", this);
        }

        public override void RemoveItem(Product product)
        {
            base.RemoveItem(product);
            Session?.SetJson("Cart", this);

        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cart");

        }
    }
}
