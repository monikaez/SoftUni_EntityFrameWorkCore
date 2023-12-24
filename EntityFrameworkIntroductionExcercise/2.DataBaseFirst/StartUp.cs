
using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Text;

public class StartUp
{
    public static void Main(string[] args)
    {
        SoftUniContext context = new();
        string output = string.Empty;

        //3. output = GetEmployeesFullInformation(context);
        //4. output = GetEmployeesWithSalaryOver50000(context);
        //5. output = GetEmployeesFromResearchAndDevelopment(context);
        //6.output = AddNewAddressToEmployee(context);
        output = GetEmployeesInPeriod(context);
        Console.WriteLine(output);

    }
    //3.public static string GetEmployeesFullInformation(SoftUniContext context) and public StartUp class. 
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.JobTitle,
                e.Salary
            }).ToList();

        string result = string.Join(Environment.NewLine, employees
            .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));

        return result;
    }



    //4.You will need method public static string GetEmployeesWithSalaryOver50000(SoftUniContext context) and public StartUp class. 
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.Salary
            }).Where(e => e.Salary > 50000)
            .OrderBy(e => e.FirstName)
            .ToList();

        return string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} - {e.Salary:f2}"));//"{firstName} – {salary}"
    }

    //5.	Employees from Research and Development
    // NOTE: You will need method public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context) and public StartUp class. 

    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var rndEmployees = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Department.Name,
                e.Salary
            }).OrderBy(e => e.Salary)
              .ThenByDescending(e => e.FirstName);
        return string.Join(Environment.NewLine, rndEmployees
            .Select(e => $"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:f2}"));
    }

    //6.Adding a New Address and Updating Employee
    //NOTE: You will need method public static string AddNewAddressToEmployee(SoftUniContext context) and public StartUp class. 
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        //Setting the address
        Address address = new Address();
        address.AddressText = "Vitoshka 15";
        address.TownId = 4;

        context.Addresses.Add(address);
        context.SaveChanges();

        // search Employee
        var searchedEmployee = context.Employees
            .Where(e => e.LastName == "Nakov")
            .FirstOrDefault();

        searchedEmployee.Address = address;
        context.SaveChanges();

        //Select employees
        var employees = context.Employees
            .Select(e => new { e.AddressId, e.Address })
            .OrderByDescending(e => e.AddressId)
            .Take(10);

        //Output
        StringBuilder sb = new StringBuilder();

        foreach (var employee in employees)
        {
            sb.AppendLine($"{employee.Address.AddressText}");
        }

        return sb.ToString().TrimEnd();
    }

    //7.7.	Employees and Projects
    // NOTE: You will need method public static string GetEmployeesInPeriod(SoftUniContext context) and public StartUp class. 
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var EmployeeInfo = context.Employees
            .Take(10)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                ManagerFirstName = e.Manager.FirstName,
                ManagerLastName = e.Manager.LastName,
                Projects = e.EmployeesProjects.Where(ep => ep.Project.StartDate.Year >= 2001 & ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project.EndDate != null
                            ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                            : "not finished"
                    })
            })
            .ToList();

        StringBuilder sb = new StringBuilder();

        foreach (var e in EmployeeInfo)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

            if (e.Projects.Any())
            {
                sb.AppendLine(String.Join(Environment.NewLine, e.Projects
                    .Select(p => $"--{p.ProjectName} - {p.StartDate} - {p.EndDate}")));
            }
        }

        return sb.ToString().TrimEnd();
    }
}