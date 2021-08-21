using System;
using System.Collections.Generic;
using FluentAssertions;
using VendingMachine;
using VendingMachine.Models;
using Xunit;

namespace UnitTests
{
    public class MachineTests
    {
        [Fact]
        public void BuyProductReturnsProductType()
        {
            var machine = new Machine();
            var product = machine.BuyProduct("Cola");

            product.Should().BeOfType<Product>();
        }

        [Theory]
        [InlineData("Cola")]
        [InlineData("Chips")]
        [InlineData("Candy")]
        public void BuyProductReturnsProductWhenSelected(string productName)
        {
            var expectedProduct = new Product() { Name = productName };

            var machine = new Machine();
            var product = machine.BuyProduct(productName);

            product.Should().BeEquivalentTo(expectedProduct);
        }

        [Theory]
        [InlineData( 0.01, 2.5, 0.75, 1.52)]
        [InlineData(0.05, 5, 0.835, 1.95)]
        [InlineData(0.10, 2.268, 0.705, 1.35)]
        [InlineData(0.25, 5.67, 0.955, 1.75)]
        [InlineData(.50, 11.34, 1.205, 2.15)]
        [InlineData(1.00, 8.1, 1.043, 2.00)]
        public void FindCoinValueIsReturnedCorrectly(double expectedValue, double weight, double diameter, double thinkness)
        {
            var machine = new Machine();
            var value = machine.FindCoinValue(weight, diameter, thinkness);

            value.Should().Be(expectedValue);
        }
    }
}
