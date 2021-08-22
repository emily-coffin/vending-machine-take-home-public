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
                totalOver =  Math.Round(totalOver, 2);

                if (Math.Round(totalOver / CoinHelper.FindCoinByName("Quarter").Value) > 0)
                {
                    var coin = CoinHelper.FindCoinByName("Quarter");
                    coinsReturned.Add(coin);
                    totalOver -= coin.Value;
                }
                else if (Math.Round(totalOver / CoinHelper.FindCoinByName("Dime").Value) > 0)
                {
                    var coin = CoinHelper.FindCoinByName("Dime");
                    coinsReturned.Add(coin);
                    totalOver -= coin.Value;
                }
                else if (Math.Round(totalOver / CoinHelper.FindCoinByName("Nickel").Value) > 0)
                {
                    var coin = CoinHelper.FindCoinByName("Nickel");
                    coinsReturned.Add(coin);
                    totalOver -= coin.Value;
                }
                else
                {
                    var coin = CoinHelper.FindCoinByName("Penny");
                    coinsReturned.Add(coin);
                    totalOver -= coin.Value;
                }
            }

            return coinsReturned;
        }

        private bool ItemInStock(string productName)
        {
            return products
                   .Any(product => (product.Name.ToLower() == productName.ToLower()));
        }
    }
}