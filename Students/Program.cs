using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<StudentSystemContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=StudentSystem;Integrated Security=True;");

        // Create a new context
        using (var context = new StudentSystemContext(optionsBuilder.Options))
        {
            // Create a new student
            var student = new Student
            {
                Name = "John Doe",
                PhoneNumber = "123-456-7890",
                RegisteredOn = DateTime.Now,
                Birthday = new DateTime(2000, 1, 1)
            };

            // Add the student to the database
            context.Students.Add(student);
            context.SaveChanges();

            // Retrieve the student from the database
            var retrievedStudent = context.Students.FirstOrDefault(s => s.Name == "John Doe");

            // Print student details
            if (retrievedStudent != null)
            {
                Console.WriteLine($"Student ID: {retrievedStudent.StudentId}");
                Console.WriteLine($"Name: {retrievedStudent.Name}");
                Console.WriteLine($"Phone Number: {retrievedStudent.PhoneNumber}");
                Console.WriteLine($"Registered On: {retrievedStudent.RegisteredOn}");
                Console.WriteLine($"Birthday: {retrievedStudent.Birthday}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }
}