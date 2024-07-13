using ConsoleApp1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        using (var context = new SoftUniContext())
        {
            string result = RemoveTown(context);
            Console.WriteLine(result);
        }
    }
    public static string RemoveTown(SoftUniContext context)
    {
        var townName = "Seattle";

       
        var employeesToUpdate = context.Employees
            .Where(e => e.Address.Town.Name == townName);

        foreach (var employee in employeesToUpdate)
        {
            employee.AddressId = null;
        }

        context.SaveChanges();

        
        var addressesToDelete = context.Addresses
            .Where(a => a.Town.Name == townName);

        int countAddressesDeleted = addressesToDelete.Count();

        context.Addresses.RemoveRange(addressesToDelete);
        context.SaveChanges();

        
        var townToDelete = context.Towns
            .FirstOrDefault(t => t.Name == townName);

        context.Towns.Remove(townToDelete);
        context.SaveChanges();

        return $"{countAddressesDeleted} addresses in {townName} were deleted";
    }


    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var sb = new StringBuilder();
        var employees = context.Employees
            .Where(e => e.FirstName.StartsWith("Sa"))
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Salary = e.Salary.ToString("F2")
            })
            .OrderBy(e=> e.FirstName)
            .ThenBy(e => e.LastName)
            .ToList();
        foreach (var employee in employees)
        {
            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
        }

        return sb.ToString().Trim();
    }
    public static string IncreaseSalaries(SoftUniContext context)
    {
        var departmentsToIncrease = new[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

        var employees = context.Employees
            .Where(e => departmentsToIncrease.Contains(e.Department.Name))
            .ToList();

        foreach (var employee in employees)
        {
            employee.Salary *= 1.12m;
        }

        context.SaveChanges();

        var updatedEmployees = employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                Salary = e.Salary
            })
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToList();

        var sb = new StringBuilder();

        foreach (var employee in updatedEmployees)
        {
            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.Salary:F2}");
        }

        return sb.ToString().Trim();
    }
    public static string GetLatestProjects(SoftUniContext context)
    {
        var sb =new StringBuilder();

        var projects = context.Projects
            .OrderByDescending(p=> p.StartDate)
            .Take(10)
            .OrderBy(p=> p.Name)
            .Select(p=> new
            {
                p.Name,
                p.Description,
                p.StartDate
            }).ToList();
        foreach (var project in projects)
        {
            sb.AppendLine($"{project.Name}");
            sb.AppendLine($"{project.Description}");
            sb.AppendLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
        }

        return sb.ToString().Trim();
    }
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var sb = new StringBuilder();
        var departments = context.Departments
            .Where(d=> d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d=> new
            {
                d.Name,
                ManagerFirstName = d.Manager.FirstName,
                ManagerLastName = d.Manager.LastName,
                Employees = d.Employees
                .OrderBy(e=> e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e=> new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle
                }).ToList()
            }).ToList();
        foreach (var department in departments)
        {
            sb.AppendLine($"{department.Name} - {department.ManagerFirstName} {department.ManagerLastName}");
            foreach (var employee in department.Employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            }
        }

        return sb.ToString().Trim();
    }


    public static string GetAddressesByTowns(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var addresses = context.Addresses
             .Include(a => a.Employees)
             .Include(a => a.Town)
             .OrderByDescending(a => a.Employees.Count)
             .ThenBy(a => a.Town.Name)
             .ThenBy(a => a.AddressText)
             .Take(10)
             .Select(a => new
             {
                 AddressText = a.AddressText,
                 TownName = a.Town.Name,
                 EmployeeCount = a.Employees.Count
             })
             .ToList();

        foreach (var address in addresses)
        {
            sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
        }

        return sb.ToString().Trim();
    }
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var adresses = context.Addresses
            .Include(a=> a.Employees)
            .Include(a=> a.Town)
            .OrderByDescending(a => a.Employees.Count())
            .ThenBy(a=> a.Town.Name)
            .ThenBy(a=> a.AddressText)
            .Take(10)
            .Select(a=> new
            {
                AddressText = a.AddressText,
                TownName = a.Town.Name,
                EmployeeCount = a.Employees.Count
            }
            ).ToList();
        foreach(var address in adresses)
        {
            sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
        }
        return sb.ToString().Trim();
    }
    public static string GetAddressesByTown(SoftUniContext context)
    {
        var sb = new StringBuilder();

        var addresses = context.Addresses
            .Include(e => e.Employees)
            .Include(a => a.Employees)
            .Include(a => a.Town)
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town.Name)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .Select(a => new
            {
                AddressText = a.AddressText,
                TownName = a.Town.Name,
                EmployeeCount = a.Employees.Count
            })
            .ToList();

        foreach(var address in addresses)
        {
            sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
        }

        return sb.ToString().Trim();
    }

    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(e => new
            {
                FullName = e.FirstName + " " + (e.MiddleName ?? "") + " " + e.LastName,
                e.JobTitle,
                Salary = Math.Round(e.Salary, 2)
            })
            .ToList();

        string result = string.Join(Environment.NewLine, employees
            .Select(e => $"{e.FullName} {e.JobTitle} {e.Salary:F2}"));

        return result;
    }
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.Salary > 50000)
            .OrderBy(e => e.FirstName)
            .Select(e => new
            {
                FirstName = e.FirstName,
                Salary = Math.Round(e.Salary, 2)
            })
            .ToList();

        string result = string.Join(Environment.NewLine, employees
            .Select(e => $"{e.FirstName} - {e.Salary:F2}"));

        return result;
    }
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .OrderBy(e => e.Salary) 
            .ThenByDescending(e => e.FirstName)
            .Select(e => new
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.Department.Name,
                Salary = Math.Round(e.Salary, 2)
            })
            .ToList();

        
        string result = string.Join(Environment.NewLine, employees
            .Select(e => $"{e.FirstName} {e.LastName} from {e.Department} - ${e.Salary:F2}"));

        return result;
    }
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

        if (employee == null)
        {
            throw new InvalidOperationException("Employee with last name Nakov not found.");
        }

        var newAddress = new Address
        {
            AddressText = "Vitoshka 15",
            TownId = 4 
        };

        
        employee.Address = newAddress;

        
        context.SaveChanges();

        
        var topAddresses = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address.AddressText)
            .ToList();

        
        string result = string.Join(Environment.NewLine, topAddresses);

        return result;
    }
}