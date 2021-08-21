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
            var product = machine.BuyProduct();

            product.Should().BeOfType<Product>();
        }
    }
}
