using System.Collections.Generic;
using FluentAssertions;
using VendingMachine;
using VendingMachine.Models;
using Xunit;

namespace UnitTests
{
    public class CoinHelperTests
    {
        [Fact]
        public void AvailableCoinsReturnsCorrectListOfCoinsAvailable()
        {
            var coin = CoinHelper.AvailableCoins;

            var expectedCoins = new List<Coin>()
            {
                new Coin() { Name = "Penny", Value = 0.01, Weight = 2.5, Diameter = 0.75, Thinkness = 1.52 },
                new Coin() { Name = "Nickel", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 },
                new Coin() { Name = "Dime", Value = 0.10, Weight = 2.268, Diameter = 0.705, Thinkness = 1.35 },
                new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
            };
            coin.Should().BeEquivalentTo(expectedCoins);
        }

        [Fact]
        public void FindCoinByNameReturnsCoinWhenGivenValidName()
        {
            var coin = CoinHelper.FindCoinByName("Nickel");

            var expectedCoin = new Coin() { Name = "Nickel", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 };
            coin.Should().BeEquivalentTo(expectedCoin);
        }

        [Theory]
        [InlineData( 0.01, 2.5, 0.75, 1.52)]
        [InlineData(0.05, 5, 0.835, 1.95)]
        [InlineData(0.10, 2.268, 0.705, 1.35)]
        [InlineData(0.25, 5.67, 0.955, 1.75)]
        [InlineData(.50, 11.34, 1.205, 2.15)]
        [InlineData(1.00, 8.1, 1.043, 2.00)]
        public void FindCoinByPropertiesReturnsCorrectCoin(double expectedValue, double weight, double diameter, double thinkness)
        {
            var value = CoinHelper.FindCoinByProperties(weight, diameter, thinkness);

            value.Should().Be(expectedValue);
        }
    }
}
