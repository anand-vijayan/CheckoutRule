using CheckoutRule.Middleware.Model;
using System.Collections.Generic;

namespace CheckoutRule.Middleware.Interfaces
{
    public interface IPromoCalculator
    {
        Cart ApplyPromosInCart(List<Item> items, List<Promo> promos, Cart cart);
    }
}
