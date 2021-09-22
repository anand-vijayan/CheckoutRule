using System.Collections.Generic;
using CheckoutRule.Middleware.Controllers;
using CheckoutRule.Middleware.Interfaces;
using CheckoutRule.Middleware.Model;
using NUnit.Framework;

namespace CheckoutRule.Test
{
    [TestFixture]
    public class CheckoutCalculatorTests
    {
        #region Test Data

        private List<Item> GetItems() => new List<Item>
        {
            new Item { ItemName = 'A', ItemPrice = 50.00m },
            new Item { ItemName = 'B', ItemPrice = 30.00m },
            new Item { ItemName = 'C', ItemPrice = 20.00m },
            new Item { ItemName = 'D', ItemPrice = 15.00m },
        };

        private List<Promo> GetPromos() => new List<Promo>
        {
            new Promo
            {
                PromoType = PromoTypes.Bundle,
                Items = new List<Item>
                {
                    new Item { ItemName = 'A', Quantity = 3 }
                },
                SpecialPrice = 130.00m
            },
            new Promo
            {
                PromoType = PromoTypes.Bundle,
                Items = new List<Item>
                {
                    new Item { ItemName = 'B', Quantity = 2 }
                },
                SpecialPrice = 45.00m
            },
            new Promo
            {
                PromoType = PromoTypes.Bundle,
                Items = new List<Item>
                {
                    new Item { ItemName = 'C', Quantity = 1 },
                    new Item { ItemName = 'D', Quantity = 1 }
                },
                SpecialPrice = 30.00m
            }
        };

        private Cart GetCart_Scenario_A() => new Cart
        {
            Items = new List<Item>
            {
                new Item { ItemName = 'A', Quantity = 1, ItemPrice = 50.00m, IsPromoApplied = false, ReducedPrice = 50.00m },
                new Item { ItemName = 'B', Quantity = 1, ItemPrice = 30.00m, IsPromoApplied = false, ReducedPrice = 30.00m },
                new Item { ItemName = 'C', Quantity = 1, ItemPrice = 20.00m, IsPromoApplied = false, ReducedPrice = 20.00m },
            },
            TotalPricePayable = 100.00m //Total based on actual price
        };

        private Cart GetCart_Scenario_B() => new Cart
        {
            Items = new List<Item>
            {
                new Item { ItemName = 'A', Quantity = 5, ItemPrice = 250.00m, IsPromoApplied = false, ReducedPrice = 230.00m },
                new Item { ItemName = 'B', Quantity = 5, ItemPrice = 150.00m, IsPromoApplied = false, ReducedPrice = 120.00m },
                new Item { ItemName = 'C', Quantity = 1, ItemPrice = 20.00m, IsPromoApplied = false, ReducedPrice = 20.00m },
            },
            TotalPricePayable = 420.00m //Total based on actual price
        };

        private Cart GetCart_Scenario_C() => new Cart
        {
            Items = new List<Item>
            {
                new Item { ItemName = 'A', Quantity = 3, ItemPrice = 150.00m, IsPromoApplied = false, ReducedPrice = 130.00m },
                new Item { ItemName = 'B', Quantity = 5, ItemPrice = 150.00m, IsPromoApplied = false, ReducedPrice = 120.00m },
                new Item { ItemName = 'C', Quantity = 1, ItemPrice = 20.00m, IsPromoApplied = false, ReducedPrice = 0.00m },
                new Item { ItemName = 'D', Quantity = 1, ItemPrice = 15.00m, IsPromoApplied = false, ReducedPrice = 30.00m },
            },
            TotalPricePayable = 335.00m //Total based on actual price
        };

        #endregion

        private readonly IPromoCalculator promoCalculator;

        public CheckoutCalculatorTests()
        {
            promoCalculator = new PromoCalculator();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateCartValue_ScenarioA()
        {
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                GetItems(),
                GetPromos(),
                GetCart_Scenario_A());

            Assert.AreEqual(100.00m, processedCart.TotalPricePayable, "Scenario A: Total price payable should be 100.00");
        }

        [Test]
        public void CalculateCartValue_ScenarioB()
        {
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                GetItems(),
                GetPromos(),
                GetCart_Scenario_B());

            Assert.AreEqual(370.00m, processedCart.TotalPricePayable, "Scenario B: Total price payable should be 370.00");
        }

        [Test]
        public void CalculateCartValue_ScenarioC()
        {
            Cart processedCart = promoCalculator.ApplyPromosInCart(
                GetItems(),
                GetPromos(),
                GetCart_Scenario_C());

            Assert.AreEqual(280.00m, processedCart.TotalPricePayable, "Scenario C: Total price payable should be 280.00");
        }
    }
}