using System;
using System.Collections.Generic;
using CheckoutRule.Middleware.Interfaces;
using CheckoutRule.Middleware.Model;

namespace CheckoutRule.Middleware.Controllers
{
    public class PromoCalculator : IPromoCalculator
    {
        private bool IsListObjectValid<T>(List<T> tObject) => (tObject != null && tObject.Count > 0);



        public Cart ApplyPromosInCart(List<Item> items, List<Promo> promos, Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
