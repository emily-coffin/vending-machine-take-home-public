using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public class Machine
    {
        private List<Product> products;

        public Machine(List<Product> products)
        {
            this.products = products;
        }

        public Product BuyProduct(string productName)
        {
            return products
                   .Where(product => product.Name == productName)
                   .FirstOrDefault();
        }

        public Coin FindCoin(double weight, double diameter, double thinkness)
        {
            List<Coin> coins = new List<Coin>()
            {
                new Coin() { Name = "Penny", Value = 0.01, Weight = 2.5, Diameter = 0.75, Thinkness = 1.52 },
                new Coin() { Name = "Nickle", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 },
                new Coin() { Name = "Dime", Value = 0.10, Weight = 2.268, Diameter = 0.705, Thinkness = 1.35 },
                new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
            };

            return coins
                   .Where(coin => coin.Weight == weight && coin.Diameter == diameter && coin.Thinkness == thinkness)
                   .FirstOrDefault();
        }

        public bool CanBuyProduct(string productName)
        {
            return products
                   .Any(product => product.Name == productName);
        }
    }
}