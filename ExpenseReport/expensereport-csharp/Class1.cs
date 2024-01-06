using System;
using System.Collections.Generic;
using Ardalis.SmartEnum;
using System.Linq;
using System.IO;

namespace expensereport_csharp;

public enum ExpenseType
{
    DINNER, BREAKFAST, CAR_RENTAL
}

public class Expense(ExpenseType2 expenseType, decimal amount)
{
    public ExpenseType type;
    public ExpenseType2 Type { get; set; } = expenseType;
    public int amount;
    public decimal Amount { get; set; } = amount;
}

public static class Constants
{
    public static class ExpenseLimits
    {
        public const decimal DINNER_EXPENSE_LIMIT = 5000m;
        public const decimal BREAKFAST_EXPENSE_LIMIT = 1000m;
        public const decimal LUNCH_EXPENSE_LIMIT = 2000m;
    }
}

public class ExpenseType2 : SmartEnum<ExpenseType2>
{
    public static readonly ExpenseType2 Dinner = new ExpenseType2(nameof(Dinner), 0, Constants.ExpenseLimits.DINNER_EXPENSE_LIMIT, true);
    public static readonly ExpenseType2 Breakfast = new ExpenseType2(nameof(Breakfast), 1, Constants.ExpenseLimits.BREAKFAST_EXPENSE_LIMIT, true);
    public static readonly ExpenseType2 CarRental = new ExpenseType2("Car Rental", 2, 0, false);
    public static readonly ExpenseType2 Lunch = new ExpenseType2(nameof(Lunch), 3, Constants.ExpenseLimits.LUNCH_EXPENSE_LIMIT, true);

    public decimal MaxExpense { get; set; }
    public bool IsMeal { get; set; }

    private ExpenseType2(string name, int value, decimal maxExpense, bool isMeal) : base(name, value)
    {
        MaxExpense = maxExpense;
        IsMeal = isMeal;
    }
}

public class ExpenseReport(TimeProvider timeProvider)
{
	private readonly TimeProvider _timeProvider = timeProvider;

  public void PrintReport(List<Expense> expenses)
  {
    PrintReportToWriter(expenses, Console.Out);
  }

	public void PrintReportToWriter(List<Expense> expenses, TextWriter writer)
    {
        decimal mealExpenses = expenses.Where(exp => exp.Type.IsMeal).Sum(exp => exp.Amount);
        decimal total = expenses.Sum(exp => exp.Amount);

        writer.WriteLine("Expenses " + _timeProvider.GetLocalNow().DateTime);

        foreach (Expense expense in expenses)
        {
            String mealOverExpensesMarker = expense.Amount > expense.Type.MaxExpense
                    ? "X"
                    : " ";

            writer.WriteLine(expense.Type.Name + "\t" + expense.amount + "\t" + mealOverExpensesMarker);

        }
        writer.WriteLine("Meal expenses: " + mealExpenses);
        writer.WriteLine("Total expenses: " + total);
    }
}

public class Program
{
    public static void Main()
    {
        var expenses = new List<Expense>();
        expenses.Add(new Expense(ExpenseType2.Breakfast, 50) { type = ExpenseType.BREAKFAST, amount = 50 });
        expenses.Add(new Expense(ExpenseType2.Dinner, 5001) { type = ExpenseType.DINNER, amount = 5001 });
        expenses.Add(new Expense(ExpenseType2.CarRental, 400) { type = ExpenseType.CAR_RENTAL, amount = 400 });

        var report = new ExpenseReport(TimeProvider.System);
        report.PrintReport(expenses);
    }
}