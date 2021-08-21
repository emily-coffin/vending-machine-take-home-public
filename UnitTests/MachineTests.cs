using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using VendingMachine;
using VendingMachine.Models;
using Xunit;

namespace UnitTests
{
    public class MachineTests
    {
        [Fact]
        public void GetProductListReturnsListOfProducts()
        {
            var expectedProducts = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
                new Product() { Name = "Chips", Price = 0.50 },
                new Product() { Name = "Candy", Price = 0.65 }
            };

            var machine = new Machine(expectedProducts, null);
            var products = machine.GetProductList();

            products.Should().BeEquivalentTo(expectedProducts);
        }

        [Fact]
        public void GetAllCoinsPaidReturnsListOfCoinsThatCustomerPassedIn()
        {
            var expectedCoinsPaid = new List<Coin>()
            {
                new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
            };

            var machine = new Machine(null, expectedCoinsPaid);
            var coinsPaid = machine.GetAllCoinsPaid();

            coinsPaid.Should().BeEquivalentTo(expectedCoinsPaid);
        }

        [Fact]
        public void BuyProductReturnsProductType()
        {
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
            };

            var coinsPaid = Enumerable
                            .Repeat(new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }, 4)
                            .ToList();


            var machine = new Machine(products, coinsPaid);
            var product = machine.BuyProduct("Cola");

            product.Should().BeOfType<Product>();
        }

        [Theory]
        [InlineData("Cola", 1.00)]
        [InlineData("Chips", 0.50)]
        [InlineData("Candy", 0.65)]
        public void BuyProductReturnsProductWhenSelectedIfAvailable(string productName, double price)
        {
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
                new Product() { Name = "Chips", Price = 0.50 },
                new Product() { Name = "Candy", Price = 0.65 }
            };

            var coinsPaid = Enumerable
                            .Repeat(new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }, 4)
                            .ToList();

            var machine = new Machine(products, coinsPaid);
            var product = machine.BuyProduct(productName);

            var expectedProduct = new Product() { Name = productName, Price = price };
            product.Should().BeEquivalentTo(expectedProduct);
        }

        [Fact]
        public void BuyProductThrowsExceptionWhenProductIsMissing()
        {
            var desiredProduct = "Cola";
            var products = new List<Product>
            {
                new Product() { Name = "Chips", Price = 0.50 },
                new Product() { Name = "Candy", Price = 0.65 }
            };

            var coinsPaid = Enumerable
                            .Repeat(new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }, 4)
                            .ToList();


            var machine = new Machine(products, coinsPaid);
            Action act = () => machine.BuyProduct(desiredProduct);

            act.Should().Throw<Exception>().WithMessage($"Unable to purchase {desiredProduct}");
        }

        [Fact]
        public void BuyProductThrowsExceptionWhenProductIsMoreThanPaymentGivenByCustomer()
        {
            var desiredProduct = "Cola";
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 },
            };

            var coinsPaid = Enumerable
                            .Repeat(new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }, 2)
                            .ToList();

            var machine = new Machine(products, coinsPaid);
            Action act = () => machine.BuyProduct(desiredProduct);

            act.Should().Throw<Exception>().WithMessage($"Unable to purchase {desiredProduct}");
        }

        [Theory]
        [InlineData("Penny", 0.01, 2.5, 0.75, 1.52)]
        [InlineData("Nickle", 0.05, 5, 0.835, 1.95)]
        [InlineData("Dime", 0.10, 2.268, 0.705, 1.35)]
        [InlineData("Quarter", 0.25, 5.67, 0.955, 1.75)]
        public void AddCoinsToPaymentAppendsCoinsEnteredByCustomer(string name, double value, double weight, double diameter, double thinkness)
        {
            var machine = new Machine(new List<Product>(), new List<Coin>());
            machine.AddCoinsToPayment(weight, diameter, thinkness);
            var coinsPaid = machine.GetAllCoinsPaid();

            var expectedCoinList = new List<Coin>()
            {
                new Coin() { Name = name, Value = value, Weight = weight, Diameter = diameter, Thinkness = thinkness }
            };
            coinsPaid.Should().BeEquivalentTo(expectedCoinList);
        }

        [Fact]
        public void MakeChangeReturnsChangeToCustomer()
        {
            var productName = "Cola";
            var products = new List<Product>
            {
                new Product() { Name = "Cola", Price = 1.00 }
            };
            var coinsPaid = Enumerable
                            .Repeat(new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 },5)
                            .ToList();

            var machine = new Machine(products, coinsPaid);
            var change = machine.MakeChange(productName);

            var expectedChange = new List<Coin>()
            {
                new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
            };
            change.Should().BeEquivalentTo(expectedChange);
        }
    }
}
