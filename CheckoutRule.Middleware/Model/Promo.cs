using System.Collections.Generic;
namespace CheckoutRule.Middleware.Model
{
    public class Promo
    {
        public PromoTypes PromoType { get; set; }
        public List<Item> Items { get; set; }
        public decimal SpecialPrice { get; set; }
    }
}
