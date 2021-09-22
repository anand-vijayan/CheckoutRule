using System;
using CheckoutRule.Middleware.Controllers;
using CheckoutRule.Middleware.Interfaces;
using CheckoutRule.Middleware.Model;

namespace CheckoutRule
{
    class Program
    {
        static void Main(string[] args)
        {
            ScenarioA();
            ScenarioB();
            ScenarioC();
        }

        private static void ScenarioA()
        {
            IPromoCalculator promoCalculator = new PromoCalculator();
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                SampleData.GetItems(),
                SampleData.GetPromos(),
                SampleData.GetCart_Scenario_A());

            if (processedCart != null)
            {
                Console.WriteLine("Scenario A");
                foreach (var cartItem in processedCart.Items)
                {
                    Console.WriteLine($"{cartItem.Quantity} * " +
                        $"{cartItem.ItemName}\t" +
                        $"{(string.IsNullOrEmpty(cartItem.Calculation) ? cartItem.ReducedPrice.ToString() : cartItem.Calculation)}");
                }
                Console.WriteLine($"Total:\t{processedCart.TotalPricePayable}");
            }
        }

        private static void ScenarioB()
        {
            IPromoCalculator promoCalculator = new PromoCalculator();
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                SampleData.GetItems(),
                SampleData.GetPromos(),
                SampleData.GetCart_Scenario_B());

            if (processedCart != null)
            {
                Console.WriteLine("Scenario B");
                foreach (var cartItem in processedCart.Items)
                {
                    Console.WriteLine($"{cartItem.Quantity} * " +
                        $"{cartItem.ItemName}\t" +
                        $"{(string.IsNullOrEmpty(cartItem.Calculation) ? cartItem.ReducedPrice.ToString() : cartItem.Calculation)}");
                }
                Console.WriteLine($"Total:\t{processedCart.TotalPricePayable}");
            }
        }

        private static void ScenarioC()
        {
            IPromoCalculator promoCalculator = new PromoCalculator();
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                SampleData.GetItems(),
                SampleData.GetPromos(),
                SampleData.GetCart_Scenario_C());

            if (processedCart != null)
            {
                Console.WriteLine("Scenario C");
                foreach (var cartItem in processedCart.Items)
                {
                    Console.WriteLine($"{cartItem.Quantity} * " +
                        $"{cartItem.ItemName}\t" +
                        $"{(string.IsNullOrEmpty(cartItem.Calculation) ? cartItem.ReducedPrice.ToString() : cartItem.Calculation)}");
                }
                Console.WriteLine($"Total:\t{processedCart.TotalPricePayable}");
            }
        }
    }
}
