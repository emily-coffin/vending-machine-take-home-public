using System;
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
            var product = machine.BuyProduct(null);

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
    }
}
