using System;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            using var context = new SalesContext();
            context.Database.EnsureCreated();

            SalesSeeder.Seed(context);

            Console.WriteLine("Database seeded successfully.");
        }
    }
}