using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public static class CoinHelper
    {
        public static List<Coin> AvailableCoins
        {
            get
            {
                return new List<Coin>()
                {
                    new Coin() { Name = "Penny", Value = 0.01, Weight = 2.5, Diameter = 0.75, Thinkness = 1.52 },
                    new Coin() { Name = "Nickel", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 },
                    new Coin() { Name = "Dime", Value = 0.10, Weight = 2.268, Diameter = 0.705, Thinkness = 1.35 },
                    new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
                };
            }
        }

        public static Coin FindCoinByName(string name)
        {
            return AvailableCoins
                   .Where(coin => coin.Name == name)
                   .FirstOrDefault();
        }

        public static Coin FindCoinByProperties(double weight, double diameter, double thinkness)
        {
            return AvailableCoins
                   .Where(coin => coin.Weight == weight && coin.Diameter == diameter && coin.Thinkness == thinkness)
                   .FirstOrDefault();
        }
    }
}