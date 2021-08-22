using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using VendingMachine.Models;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteHeaderLine();
            var machine = new Machine(new List<Product>
                                      {
                                        new Product() { Name = "Cola", Price = 1.00 },
                                        new Product() { Name = "Chips", Price = 0.50 },
                                        new Product() { Name = "Candy", Price = 0.65 }
                                      },
                                      new List<Coin>());

            DisplayPaymentOptions(machine);
            WriteBlankLines(1);
            TakePayment(machine);
            WriteHeaderLine();
        }

        private static void DisplayProductsAvailable(Machine machine)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            System.Console.WriteLine($"The following Products are available:");

            var productsAvailable = machine.GetProductList;
            foreach (Product product in productsAvailable)
            {
                System.Console.WriteLine($"{product.Name} - {product.Price.ToString("C", CultureInfo.CurrentCulture)}"); //Displays in USD
            }
            Console.ResetColor();

            WriteHeaderLine();
        }

        private static void DisplayPaymentOptions(Machine machine)
        {
            System.Console.WriteLine($"Please enter coins:");
            System.Console.WriteLine($"1. Penny");
            System.Console.WriteLine($"2. Nickel");
            System.Console.WriteLine($"3. Dime");
            System.Console.WriteLine($"4. Quarter");
            System.Console.WriteLine($"5. Done");
        }

        public static void TakePayment(Machine machine)
        {
            var adding = true;
            while (adding)
            {
                var totalCoinsGiven = machine.GetAllCoinsPaid.Sum(coin => coin.Value);
                if (totalCoinsGiven == 0)
                {
                    System.Console.WriteLine("INSERT COIN");
                }
                else
                {
                    System.Console.WriteLine($"Total Amount Given: {totalCoinsGiven.ToString("C", CultureInfo.CurrentCulture)}");
                }

                var input = Convert.ToInt32(Console.ReadLine());

                if (input == 5)
                {
                    var productBought = BuyProduct(machine);
                    if(productBought)
                    {
                        break;
                    }
                }

                var possibleCoins = new String[] { "Penny", "Nickel", "Dime", "Quarter" };
                var coin = CoinHelper.FindCoinByName(possibleCoins[input - 1]);

                if(coin.Name == "Penny")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid Coin Was Given.");
                    System.Console.WriteLine($"CHANGE: {coin.Value.ToString("C", CultureInfo.CurrentCulture)}"); // Display in USD
                    Console.ResetColor();
                }
                else
                {
                    machine.AddCoinsToPayment(coin.Weight, coin.Diameter, coin.Thinkness);
                }
            }
        }

        private static bool BuyProduct(Machine machine)
        {
            DisplayProductsAvailable(machine);
            System.Console.WriteLine($"Which product would you like? (use product name)");
            var productChosen = Console.ReadLine().ToString();

            try
            {
                WriteBlankLines(1);

                var change = machine.MakeChange(productChosen);
                var product = machine.BuyProduct(productChosen);

                System.Console.WriteLine($"PRODUCT: {productChosen}");
                System.Console.WriteLine($"CHANGE: {change.Sum(x => x.Value).ToString("C", CultureInfo.CurrentCulture)}"); // Display in USD
                System.Console.WriteLine($"COINS GIVEN: {JsonConvert.SerializeObject(change.Select(coin => coin.Name).ToList())}");

                WriteBlankLines(1);

                System.Console.WriteLine("THANK YOU");

                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                return false;
            }
        }

        private static void WriteHeaderLine()
        {
            WriteBlankLines(1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            WriteBlankLines(1);
        }

        private static void WriteBlankLines(int times)
        {
            for(int i = 0; i <= times; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
