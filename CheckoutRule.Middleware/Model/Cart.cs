using System.Collections.Generic;

namespace CheckoutRule.Middleware.Model
{
    public class Cart
    {
        public List<Item> Items { get; set; }
        public decimal TotalPricePayable { get; set; }
        public decimal TotalPriceActual { get; set; }
    }
}
