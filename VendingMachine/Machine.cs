using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public class Machine
    {
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
            if(!ItemInStock(productName))
            {
                throw new Exception($"Sorry we are sold out of {productName}.");
            }

            if(!CanBuyProduct(productName))
            {
                throw new Exception($"Please enter more coins for {productName}.");
            }

            var prodcut = products
                          .Where(x => x.Name.ToLower() == productName.ToLower())
                          .FirstOrDefault();

            products.Remove(prodcut);

            return prodcut;
        }

        public bool AddCoinsToPayment(double weight, double diameter, double thinkness)
        {
            var coin = CoinHelper.FindCoinByProperties(weight, diameter, thinkness);

            if(coin.Name == "Penny")
            {
                return false;
            }

            coinsPaid.Add(coin);

            return true;
        }

        public List<Coin> MakeChange(string productName)
        {
            var prodcutCost = Math.Round(products
                              .Where(product => product.Name.ToLower() == productName.ToLower())
                              .FirstOrDefault()
                              .Price, 2);
            var totalPaid = Math.Round(coinsPaid.Sum(coin => coin.Value), 2);

            List<Coin> coinsReturned = FindCoinsForChange(prodcutCost, totalPaid);

            return coinsReturned;
        }

        private static List<Coin> FindCoinsForChange(double prodcutCost, double totalPaid)
        {
            var coinsReturned = new List<Coin>();

            if(totalPaid > prodcutCost)
            {
                var sortedCoins = CoinHelper.AvailableCoins.OrderByDescending(coin => coin.Value);
                var totalOver = totalPaid - prodcutCost;
                var totalLeft = totalOver;

                while (totalLeft >= totalOver)
                {
                    foreach (Coin coin in sortedCoins)
                    {
                        if (coin.Value == totalLeft)
                        {
                            coinsReturned.Add(coin);
                            totalLeft -= coin.Value;
                        }
                        else if (coin.Value < totalLeft)
                        {
                            coinsReturned.Add(coin);
                            totalLeft -= coin.Value;
                        }
                    }
                }
            }

            return coinsReturned;
        }

        private bool ItemInStock(string productName)
        {
            return products
                   .Any(product => (product.Name.ToLower() == productName.ToLower()));
        }

        private bool CanBuyProduct(string productName)
        {
            return products
                   .Any(product => product.Price <= Math.Round(coinsPaid.Sum(coin => coin.Value), 2));
        }
    }
}