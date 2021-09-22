using System;
using System.Collections.Generic;
using CheckoutRule.Middleware.Interfaces;
using CheckoutRule.Middleware.Model;

namespace CheckoutRule.Middleware.Controllers
{
    public class PromoCalculator : IPromoCalculator
    {
        #region Private Methods

        private bool IsListObjectValid<T>(List<T> tObject) => (tObject != null && tObject.Count > 0);

        private void ApplyBundlePromo(List<Item> items, List<Promo> promos, Cart cart)
        {
            throw new NotImplementedException();
        }

        private void ApplyComboPromo(List<Item> items, List<Promo> promos, Cart cart)
        {
            throw new NotImplementedException();
        }

        #endregion

        public Cart ApplyPromosInCart(List<Item> items, List<Promo> promos, Cart cart)
        {
            if (cart != null
                && IsListObjectValid(cart.Items)
                && IsListObjectValid(items)
                && IsListObjectValid(promos))
            {
                ApplyBundlePromo(items, promos, cart);
                ApplyComboPromo(items, promos, cart);
            }
            else
            {
                Console.WriteLine("ApplyPromosInCart: Input not valid!!!");
            }

            return cart;
        }
    }
}
