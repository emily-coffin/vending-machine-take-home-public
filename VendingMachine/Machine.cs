using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<Product> GetProductList
        {
            get
            {
                return products;
            }
        }

        public List<Coin> GetAllCoinsPaid
        {
            get
            {
                return coinsPaid;
            }
        }

        public Product BuyProduct(string productName)
        {
            if(!ItemInStock(productName))
            {
                throw new Exception($"Sorry we are sold out of {productName}.");
            }

            var prodcut = products
                          .Where(x => x.Name.ToLower() == productName.ToLower())
                          .FirstOrDefault();

            if(prodcut.Price > coinsPaid.Sum(coin => coin.Value))
            {
                var coinsNeeded = prodcut.Price - coinsPaid.Sum(coin => coin.Value);
                throw new Exception($"Please enter {coinsNeeded.ToString("C", CultureInfo.CurrentCulture)} more for {productName}.");
            }

            return prodcut;
        }

        public bool RemoveProduct(Product product)
        {
            return products.Remove(product);
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
            var prodcutCost = products
                              .Where(product => product.Name.ToLower() == productName.ToLower())
                              .FirstOrDefault()
                              .Price;
            var totalPaid = coinsPaid.Sum(coin => coin.Value);

            List<Coin> coinsReturned = FindCoinsForChange(prodcutCost, totalPaid);

            return coinsReturned;
        }

        private static List<Coin> FindCoinsForChange(double prodcutCost, double totalPaid)
        {
            var totalOver = Math.Round(totalPaid - prodcutCost, 2);
            var coinsReturned = new List<Coin>();

            while (totalOver > 0)
            {
                totalOver = Math.Round(totalOver, 2);

                if (TotalDivisibleByCoinValue("Quarter", totalOver))
                {
                    totalOver = AddCoinToChangeList("Quarter", totalOver, coinsReturned);
                }
                else if (TotalDivisibleByCoinValue("Dime", totalOver))
                {
                    totalOver = AddCoinToChangeList("Dime", totalOver, coinsReturned);
                }
                else if (TotalDivisibleByCoinValue("Nickel", totalOver))
                {
                    totalOver = AddCoinToChangeList("Nickel", totalOver, coinsReturned);
                }
                else
                {
                    totalOver = AddCoinToChangeList("Penny", totalOver, coinsReturned);
                }
            }

            return coinsReturned;
        }

        private static bool TotalDivisibleByCoinValue(string coinName, double totalOver)
        {
            return Math.Floor(totalOver / CoinHelper.FindCoinByName(coinName).Value) > 0;
        }

        private static double AddCoinToChangeList(string coinName, double totalOver, List<Coin> coinsReturned)
        {
            var coin = CoinHelper.FindCoinByName(coinName);
            coinsReturned.Add(coin);
            totalOver -= coin.Value;

            return totalOver;
        }

        private bool ItemInStock(string productName)
        {
            return products
                   .Any(product => (product.Name.ToLower() == productName.ToLower()));
        }
    }
}