using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public class Machine
    {
        private List<Product> products = new List<Product>
        {
            new Product() { Name = "Cola", Price = 1.00 },
            new Product() { Name = "Chips", Price = 0.50 },
            new Product() { Name = "Candy", Price = 0.65 }
        };

        public Product BuyProduct(string productName)
        {


            return products
                   .Where(product => product.Name == productName)
                   .FirstOrDefault();
        }

        public double FindCoinValue(double weight, double diameter, double thinkness)
        {
            List<Coin> coins = new List<Coin>()
            {
                new Coin() { Name = "Penny", Value = 0.01, Weight = 2.5, Diameter = 0.75, Thinkness = 1.52 },
                new Coin() { Name = "Nickle", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 },
                new Coin() { Name = "Dime", Value = 0.10, Weight = 2.268, Diameter = 0.705, Thinkness = 1.35 },
                new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 },
                new Coin() { Name = "Half Dollar", Value = 0.50, Weight = 11.34, Diameter = 1.205, Thinkness = 2.15 },
                new Coin() { Name = "Dollar Coin", Value = 1.00, Weight = 8.1, Diameter = 1.043, Thinkness = 2.00 }
            };

            return coins
                   .Where(coin => coin.Weight == weight && coin.Diameter == diameter && coin.Thinkness == thinkness)
                   .Select(coin => coin.Value)
                   .FirstOrDefault();
        }

        public bool CanBuyProduct(string productName)
        {
            return products
                   .Any(product => product.Name == productName);
        }
    }
}