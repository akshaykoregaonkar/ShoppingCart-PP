using NUnit.Framework;
using ShoppingCart.Core.Calculators.Shipping;

namespace ShoppingCart.Tests.Core.Calculators.Shipping
{
    class ShippingCalculatorTest
    {
        [Test]
        public void WithBasketTotalLessThanTwenty_CheckShippingCost()
        {
            var shippingCalculator = new ShippingCalculator(19);
            var shippingCost = shippingCalculator.Calculate();
            Assert.AreEqual(7m, shippingCost);
        }

        [Test]
        public void WithBasketTotalLessThanFourty_CheckShippingCost()
        {
            var shippingCalculator = new ShippingCalculator(20);
            var shippingCost = shippingCalculator.Calculate();
            Assert.AreEqual(5m, shippingCost);
        }

        [Test]
        public void WithBasketTotalMoreThanFourty_CheckShippingCost()
        {
            var shippingCalculator = new ShippingCalculator(40);
            var shippingCost = shippingCalculator.Calculate();
            Assert.AreEqual(0m, shippingCost);
        }
    }
}
