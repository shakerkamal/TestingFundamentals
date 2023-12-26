﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public interface IEmployeeStorage
{
    void DeleteEmployee(int id);
}

public class EmployeeStorage : IEmployeeStorage
{
    private readonly EmployeeContext _db;

    public EmployeeStorage(EmployeeContext db)
    {
        _db = db;
    }

    public void DeleteEmployee(int id)
    {
        var employee = _db.Employees.Find(id);
        if (employee is null) return;
        _db.Employees.Remove(employee);
        _db.SaveChanges();
    }
}
