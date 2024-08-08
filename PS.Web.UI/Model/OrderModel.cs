using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PS.Web.UI.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;

        [BindNever]
        public Cart? Cart { get; set; } = null!;


        public string CartName { get; set; } 
        public string CartNumber { get; set; } 
        public string ExpirationMonth { get; set; } 
        public string ExpirationYear { get; set; }
        public string Cvc {  get; set; }



    }
}
