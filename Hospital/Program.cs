using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase
{
    internal class StartUp
    {
        static void Main()
        {
            using var db = new HospitalContext();
            db.Database.EnsureCreated();

            Console.WriteLine("Hospital database created successfully.");
        }
    }
}