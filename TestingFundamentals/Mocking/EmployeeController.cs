using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public class EmployeeController
{
    //private EmployeeContext _db;
    private readonly IEmployeeStorage _employeeStorage;
    public EmployeeController(IEmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
        //_db = new EmployeeContext();
    }

    public ActionResult DeleteEmployee(int id)
    {
        //var employee = _db.Employees.Find(id);
        //_db.Employees.Remove(employee);
        //_db.SaveChanges();
        _employeeStorage.DeleteEmployee(id);
        return RedirectToAction("Employess");
    }

    private ActionResult RedirectToAction(string employees)
    {
        return new RedirectResult();
    }
}

public class ActionResult { }

public class RedirectResult : ActionResult { }

public class EmployeeContext
{
    public DbSet<Employee> Employees { get; set; }
    public void SaveChanges()
    {

    }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}
