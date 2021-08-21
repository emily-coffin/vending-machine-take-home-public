using System;
using Xunit;

namespace UnitTests
{
    public class MachineTests
    {
        [Fact]
        public void BuyProductReturnsProductWhenSelected()
        {
            var machine = new Machine();
            var product = machine.BuyProduct();

            product.Should().Be(new Product())
        }
    }
}
