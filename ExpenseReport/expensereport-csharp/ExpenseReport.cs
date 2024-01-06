using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace expensereport_csharp;

public class ExpenseReport(TimeProvider timeProvider)
{
	private readonly TimeProvider _timeProvider = timeProvider;

	public void PrintReport(List<Expense> expenses)
	{
		PrintReportToWriter(expenses, Console.Out);
	}

	public void PrintReportToWriter(List<Expense> expenses, TextWriter writer)
	{
		writer.WriteLine("Expenses " + _timeProvider.GetLocalNow().DateTime);

		foreach (Expense expense in expenses)
		{
			String mealOverExpensesMarker = expense.Amount > expense.Type.MaxExpense
							? "X"
							: " ";

			writer.WriteLine($"{expense.Type.Name}\t{expense.Amount}\t{mealOverExpensesMarker}");
		}

		decimal mealExpenses = expenses.Where(exp => exp.Type.IsMeal).Sum(exp => exp.Amount);
		writer.WriteLine($"Meal expenses: {mealExpenses}");

		decimal total = expenses.Sum(exp => exp.Amount);
		writer.WriteLine($"Total expenses: {total}");
	}
}
