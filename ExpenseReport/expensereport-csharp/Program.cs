using System;
using System.Collections.Generic;

namespace expensereport_csharp;

public class Program
{
    public static void Main()
    {
        var expenses = new List<Expense>();
        expenses.Add(new Expense(ExpenseType.Breakfast, 50));
        expenses.Add(new Expense(ExpenseType.Dinner, 5001));
        expenses.Add(new Expense(ExpenseType.CarRental, 400));

        var report = new ExpenseReport(TimeProvider.System);
        report.PrintReport(expenses);
    }
}