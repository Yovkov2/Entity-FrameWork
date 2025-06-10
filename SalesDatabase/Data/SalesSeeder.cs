using System;
using System.Linq;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public static class SalesSeeder
    {
        public static void Seed(SalesContext context)
        {
            if (!context.Products.Any())
            {
                var products = new[]
                {
                    new Product { Name = "Laptop", Quantity = 10, Price = 1500 },
                    new Product { Name = "Phone", Quantity = 50, Price = 600 },
                    new Product { Name = "Tablet", Quantity = 30, Price = 400 }
                };

                var customers = new[]
                {
                    new Customer { Name = "Alice", Email = "alice@mail.com", CreditCardNumber = "1234567890123456" },
                    new Customer { Name = "Bob", Email = "bob@mail.com", CreditCardNumber = "6543210987654321" }
                };

                var stores = new[]
                {
                    new Store { Name = "Tech Store" },
                    new Store { Name = "Gadget Shop" }
                };

                context.AddRange(products);
                context.AddRange(customers);
                context.AddRange(stores);
                context.SaveChanges();

                var sales = new[]
                {
                    new Sale { Product = products[0], Customer = customers[0], Store = stores[0], Date = DateTime.Now },
                    new Sale { Product = products[1], Customer = customers[1], Store = stores[1], Date = DateTime.Now }
                };

                context.Sales.AddRange(sales);
                context.SaveChanges();
            }
        }
    }
}