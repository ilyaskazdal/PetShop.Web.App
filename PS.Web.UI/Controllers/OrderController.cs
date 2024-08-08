using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using PS.Data.Abstract;
using PS.Entity;
using PS.Web.UI.Model;

namespace PS.Web.UI.Controllers
{
    public class OrderController : Controller
    {
        private Cart cart;
        private IOrderRepo _orderRepo;
        public OrderController(Cart cartService, IOrderRepo orderRepo)
        {
            cart = cartService;
            _orderRepo = orderRepo;
        }

        public IActionResult Checkout()
        {
            return View(new OrderModel() { Cart = cart});
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model) {
            if (cart.Items.Count==0) 
            {
                ModelState.AddModelError("", "Sepetiniz boş");
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = model.Name,
                    City = model.City,
                    Phone = model.Phone,
                    Address = model.Address,
                    OrderDate = model.OrderDate,
                    OrderItems = cart.Items.Select(i => new PS.Entity.OrderItem
                    {
                        ProductId = i.Product.ProductId,
                        Price =(double)i.Product.UnitPrice,
                        Quantity = i.Quantity

                    }).ToList()
                };
                model.Cart = cart;
                var payment = ProcessPayment(model);
                if (payment.Status == "success")
                {
                    _orderRepo.TakeOrder(order);
                    cart.Clear();
                    return RedirectToPage("/Complete", new { OrderId = order.OrderId });

                }

                model.Cart = cart;
                return View(model);


            }
            else
            {
                model.Cart = cart;
                return View(model);
            }
           
        }
        private Payment ProcessPayment(OrderModel model) {
            Options options = new Options();
            options.ApiKey = "sandbox-hBuW37tZXDjuOH5mGHmYJnuNxBrL2f57";
            options.SecretKey = "sandbox-2KLAmo0Ra5FrR4xrNkXsqqBRBjqb4R6x";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111,999999999).ToString();
            request.Price = model?.Cart?.CalculateTotal().ToString();
            request.PaidPrice = model?.Cart?.CalculateTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model?.CartName;
            paymentCard.CardNumber = model?.CartNumber;
            paymentCard.ExpireMonth = model?.ExpirationMonth;
            paymentCard.ExpireYear = model?.ExpirationYear;
            paymentCard.Cvc = model?.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = model?.OrderId.ToString(); // orderid teknik olarak buyerında idsi çünki üyelik kullanamdan öedeme alıyoruz OrderModel içine User users getsetine gekrek yok  
            buyer.Name = model?.Name; 
            buyer.Surname = "Doe";                // surname koymamışım
            buyer.GsmNumber = model?.Phone;
            buyer.Email = "email@email.com";      // emailda koymamışım aslında email gerekli mesaj göndermek istenirse satınalımla ilgili
            buyer.IdentityNumber = "74300864791";  // bunun ne olduğunu bilmiyorum bi bak --// TC numarası sanırsam oda yok
            buyer.LastLoginDate = "2015-10-05 12:43:35"; // buda yok
            buyer.RegistrationDate = "2013-04-21 15:12:09"; // yok
            buyer.RegistrationAddress = model?.Address; // normal adress satırı var elimde
            buyer.Ip = "85.34.78.112";             // ip nasıl alınacak bilmiyorum
            buyer.City = model?.City;
            buyer.Country = model?.City;           // sadece city almışım
            buyer.ZipCode = "34732";                // zipcodeda istemedim
            request.Buyer = buyer;
            
            Address shippingAddress = new Address();
            shippingAddress.ContactName = model?.Name;
            shippingAddress.City = model?.City;
            shippingAddress.Country = "Turkey";            // ülkeyi hiiç düşünmedim sadece türkiye olur diye
            shippingAddress.Description = model?.Address;  // adress text olarak alınıyor 
            shippingAddress.ZipCode = "34742";             // yine zipcode üstekigibi yok
            request.ShippingAddress = shippingAddress;   

            Address billingAddress = new Address();
            billingAddress.ContactName = model?.Name;
            billingAddress.City = model?.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model?.Address;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress; //burası shipping adressle aynı olcak neden farklı bilmiyorum ama olanları doldurdum nede osla hata vermiyor ödeme alabiliyorum

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach(var item in model?.Cart?.Items ?? Enumerable.Empty<CartItem>())
            {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = item.Product.ProductId.ToString() ;
            firstBasketItem.Name = item.Product.ProductName;
            firstBasketItem.Category1 = item.Product.CategoryId.ToString();
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = item.Product.UnitPrice.ToString(); //string olması lzm 100.0 olarak gelmesin 100 olrak gelsin .dan sonra 0ı kabul etmiyormuş
            basketItems.Add(firstBasketItem);
            }

            

            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            return payment;
        }
    }
}
