using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (Item cartItem in cart.Items.Where(a => !a.IsPromoApplied))
            {
                foreach (var bundlePromoItem in promos.Where(a => a.PromoType == PromoTypes.Bundle))
                {
                    if (bundlePromoItem.Items.Where(a => a.ItemName == cartItem.ItemName
                        && cartItem.Quantity >= a.Quantity).Any())
                    {
                        int numberOfBundle = cartItem.Quantity / bundlePromoItem.Items.First(a => a.ItemName == cartItem.ItemName).Quantity;
                        int numberOfItemsWithoutDiscount = cartItem.Quantity % bundlePromoItem.Items.First(a => a.ItemName == cartItem.ItemName).Quantity;

                        decimal actualPrice = items.First(a => a.ItemName == cartItem.ItemName).ItemPrice;
                        decimal bundleTotal = numberOfBundle * bundlePromoItem.SpecialPrice;

                        cartItem.ReducedPrice = bundleTotal + (numberOfItemsWithoutDiscount * actualPrice);
                        cartItem.IsPromoApplied = true;
                        cartItem.Calculation = $"({numberOfBundle} * {bundlePromoItem.SpecialPrice}) + ({numberOfItemsWithoutDiscount} * {actualPrice})";
                    }
                }
            }

            cart.TotalPriceActual = cart.TotalPricePayable;
            cart.TotalPricePayable = cart.Items.Sum(a => a.ReducedPrice);
        }

        private void ApplyComboPromo(List<Item> items, List<Promo> promos, Cart cart)
        {
            foreach (var comboPromo in promos.Where(a => a.PromoType == PromoTypes.Combo))
            {
                int cartItemsFound = 0, iCount = 1;
                List<char> promoAppliedItems = new List<char>();

                foreach (var comboPromoItem in comboPromo.Items)
                {
                    if(cart.Items.Where(a => a.ItemName == comboPromoItem.ItemName
                        && a.Quantity >= comboPromoItem.Quantity
                        && !a.IsPromoApplied).Any())
                    {
                        cartItemsFound++;
                        promoAppliedItems.Add(comboPromoItem.ItemName);
                    }
                }

                if (cartItemsFound == comboPromo.Items.Count)
                {
                    foreach (var item in promoAppliedItems)
                    {
                        if (iCount != cartItemsFound)
                        {
                            cart.Items.First(a => a.ItemName == item).ReducedPrice = 0.00m;
                            iCount++;
                        }
                        else
                        {
                            cart.Items.First(a => a.ItemName == item).ReducedPrice = comboPromo.SpecialPrice;
                        }
                    }
                }

                cart.TotalPriceActual = cart.TotalPricePayable;
                cart.TotalPricePayable = cart.Items.Sum(a => a.ReducedPrice);
            }
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
