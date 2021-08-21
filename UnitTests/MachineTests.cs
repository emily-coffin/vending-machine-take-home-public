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
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
            };

            var machine = new Machine(products);
            var product = machine.BuyProduct("Cola");

            product.Should().BeOfType<Product>();
        }

        [Theory]
        [InlineData("Cola", 1.00)]
        [InlineData("Chips", 0.50)]
        [InlineData("Candy", 0.65)]
        public void BuyProductReturnsProductWhenSelected(string productName, double price)
        {
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
                new Product() { Name = "Chips", Price = 0.50 },
                new Product() { Name = "Candy", Price = 0.65 }
            };

            var machine = new Machine(products);
            var product = machine.BuyProduct(productName);

            var expectedProduct = new Product() { Name = productName, Price = price };
            product.Should().BeEquivalentTo(expectedProduct);
        }

        [Theory]
        [InlineData("Penny", 0.01, 2.5, 0.75, 1.52)]
        [InlineData("Nickle", 0.05, 5, 0.835, 1.95)]
        [InlineData("Dime", 0.10, 2.268, 0.705, 1.35)]
        [InlineData("Quarter", 0.25, 5.67, 0.955, 1.75)]
        public void FindCoinIsReturnsCorrectCoinWhenGivenCoinProperties(string expectedName, double expectedValue, double weight, double diameter, double thinkness)
        {
            var machine = new Machine(null);
            var coin = machine.FindCoin(weight, diameter, thinkness);

            coin.Name.Should().Be(expectedName);
            coin.Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("Cola", true)]
        [InlineData("Chips", true)]
        [InlineData("Candy", true)]
        [InlineData("Cola", false)]
        [InlineData("Chips", false)]
        [InlineData("Candy", false)]
        public void CanBuyProductReturnsBoolResponseIfItemIsAvailable(string productName, bool expectedAvailability)
        {
            var products = new List<Product>()
            {
                new Product() { Name = "FakeProduct" }
            };

            if (expectedAvailability == true)
            {
                products.Add(new Product() { Name = productName });
            }

            var machine = new Machine(products);
            var isAvailable = machine.CanBuyProduct(productName);

            isAvailable.Should().Be(expectedAvailability);
        }

        // [Theory]
        // [InlineData("Penny", 0.01, 2.5, 0.75, 1.52)]
        // [InlineData("Nickle", 0.05, 5, 0.835, 1.95)]
        // [InlineData("Dime", 0.10, 2.268, 0.705, 1.35)]
        // [InlineData("Quarter", 0.25, 5.67, 0.955, 1.75)]
        // public void AddCoinsToPaymentAppendsCoinsEnteredByCustomer(double expectedValue, double weight, double diameter, double thinkness)
        // {
        //     var machine = new Machine(null);
        //     var value = machine.AddCoinsToPayment(weight, diameter, thinkness);

        //     var expectedCoinList =
        //     return
        // }
    }
}
