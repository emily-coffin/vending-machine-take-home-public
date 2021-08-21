using System.Collections.Generic;
using System.Linq;
using VendingMachine.Models;

namespace VendingMachine
{
    public class Machine
    {
        public Product BuyProduct(string productName)
        {
            var products = new List<Product>
            {
                new Product() { Name = "Cola"},
                new Product() { Name = "Chips"},
                new Product() { Name = "Candy"}
            };

            return products
                   .Where(product => product.Name == productName)
                   .FirstOrDefault();
        }
    }
}