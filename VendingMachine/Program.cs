using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            // Happy path
            System.Console.WriteLine($"\n\n ------------------------");
            var machine = new Machine(new List<Product>
                                      {
                                        new Product() { Name = "Cola", Price = 1.00 },
                                        new Product() { Name = "Chips", Price = 0.50 },
                                        new Product() { Name = "Candy", Price = 0.65 }
                                      },
                                      new List<Coin>());

            var productsAvailable = machine.GetProductList();
            System.Console.WriteLine($"The following Products are available:");
            foreach(Product product in productsAvailable)
            {
               System.Console.WriteLine($"{product.Name} - {Math.Round(product.Price, 2)}");
            }

            System.Console.WriteLine($"\n\n");

            System.Console.WriteLine($"Please enter coins: \n 1. Nickel \n 2. Dime \n 3. Quarter \n 4. Done \n\n");
            var adding = true;
            while(adding)
            {
                System.Console.WriteLine("INSERT COIN");
                var input = Convert.ToInt32(Console.ReadLine());

                if(input == 4)
                {
                    break;
                }
                var possibleCoins = new String[] { "Nickel", "Dime", "Quarter" };
                var coin = machine.FindCoinByName(possibleCoins[input - 1]);
                machine.AddCoinsToPayment(coin.Weight, coin.Diameter, coin.Thinkness);
            }

            System.Console.WriteLine($"Which product would you like (use product name)");
            var productChosen = Console.ReadLine().ToString();

            try
            {
                System.Console.WriteLine($"\n\n");
                
                var change = machine.MakeChange(productChosen);
                var product = machine.BuyProduct(productChosen);

                System.Console.WriteLine($"Product: {productChosen}");
                System.Console.WriteLine($"CHANGE: ${Math.Round(change.Sum(x => x.Value), 2)}");
                System.Console.WriteLine("THANK YOU");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("There was an error. Please try again. " + ex.Message);
            }

            System.Console.WriteLine($"\n\n ------------------------");
        }
    }
}
