using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public class Machine
    {
        private List<Coin> coins = new List<Coin>()
        {
            new Coin() { Name = "Penny", Value = 0.01, Weight = 2.5, Diameter = 0.75, Thinkness = 1.52 },
            new Coin() { Name = "Nickle", Value = 0.05, Weight = 5, Diameter = 0.835, Thinkness = 1.95 },
            new Coin() { Name = "Dime", Value = 0.10, Weight = 2.268, Diameter = 0.705, Thinkness = 1.35 },
            new Coin() { Name = "Quarter", Value = 0.25, Weight = 5.67, Diameter = 0.955, Thinkness = 1.75 }
        };

        private List<Product> products = new List<Product>();
        private List<Coin> coinsPaid = new List<Coin>();

        public Machine(List<Product> products, List<Coin> coinsPaid)
        {
            this.products = products;
            this.coinsPaid = coinsPaid;
        }

        public List<Product> GetProductList()
        {
            return products;
        }

        public List<Coin> GetAllCoinsPaid()
        {
            return coinsPaid;
        }

        public Product BuyProduct(string productName)
        {
            if(!CanBuyProduct(productName))
            {
                throw new Exception($"Unable to purchase {productName}");
            }
            
            var prodcut = products
                          .Where(x => x.Name == productName)
                          .FirstOrDefault();

            products.Remove(prodcut);

            return prodcut;
        }

        public void AddCoinsToPayment(double weight, double diameter, double thinkness)
        {
            var coin = FindCoin(weight, diameter, thinkness);
            coinsPaid.Add(coin);
        }

        public List<Coin> MakeChange(string productName)
        {
            var prodcutCost = Math.Round(products
                              .Where(product => product.Name == productName)
                              .FirstOrDefault()
                              .Price, 2);
            var totalPaid = Math.Round(coinsPaid.Sum(coin => coin.Value), 2);

            if(totalPaid > prodcutCost)
            {
                var sortedCoins = coins.OrderByDescending(coin => coin.Value);
                var totalOver = totalPaid - prodcutCost;
                var coinsReturned = new List<Coin>();
                var totalLeft = totalOver;

                while(totalLeft >= totalOver)
                {
                    foreach(Coin coin in sortedCoins)
                    {
                        if(coin.Value == totalLeft)
                        {
                            coinsReturned.Add(coin);
                            totalLeft -= coin.Value;
                        }
                        else if(coin.Value < totalLeft)
                        {
                            coinsReturned.Add(coin);
                            totalLeft -= coin.Value;
                        }
                    }
                }

                return coinsReturned;
            }

            return null;
        }

        private bool CanBuyProduct(string productName)
        {
            return products
                   .Any(product => (product.Name == productName) &&
                                   (product.Price <= Math.Round(coinsPaid.Sum(coin => coin.Value), 2)));
        }

        private Coin FindCoin(double weight, double diameter, double thinkness)
        {
            return coins
                   .Where(coin => coin.Weight == weight && coin.Diameter == diameter && coin.Thinkness == thinkness)
                   .FirstOrDefault();
        }

    }
}