using NUnit.Framework;
using expensereport_csharp;
using System;
using System.IO;
using System.Collections.Generic;

public class ExpenseReport_PrintReportToWriter
{
	private ExpenseReport GetExpenseReport()
	{
		return new ExpenseReport(TimeProvider.System);
	}

	[Test]
	public void AddsXToDinnerOverLimit()
	{
		var report = GetExpenseReport();
		decimal expenseAmount = Constants.ExpenseLimits.DINNER_EXPENSE_LIMIT + 1;

		var expense = new Expense(ExpenseType.Dinner, expenseAmount);

		var writer = new StringWriter();
		report.PrintReportToWriter([expense], writer);

		string result = writer.ToString();

		Assert.True(result.Contains($"Dinner\t{expenseAmount}\tX"));
	}

	[Test]
	public void AddsXToLunchOverLimit()
	{
		var report = GetExpenseReport();
		decimal expenseAmount = Constants.ExpenseLimits.LUNCH_EXPENSE_LIMIT + 1;

		var expense = new Expense(ExpenseType.Lunch, expenseAmount);

		var writer = new StringWriter();
		report.PrintReportToWriter([expense], writer);

		string result = writer.ToString();

		Assert.True(result.Contains($"Lunch\t{expenseAmount}\tX"));
	}

	[Test]
	public void AddsXToBreakfastOverLimit()
	{
		var report = GetExpenseReport();
		decimal expenseAmount = Constants.ExpenseLimits.BREAKFAST_EXPENSE_LIMIT + 1;

		var expense = new Expense(ExpenseType.Breakfast, expenseAmount);

		var writer = new StringWriter();
		report.PrintReportToWriter([expense], writer);

		string result = writer.ToString();

		Assert.True(result.Contains($"Breakfast\t{expenseAmount}\tX"));
	}

	[Test]
	public void DoesNotAddXToMealsAtLimit()
	{
		var report = GetExpenseReport();

		var expenses = new List<Expense>(
			[new Expense(ExpenseType.Breakfast, Constants.ExpenseLimits.BREAKFAST_EXPENSE_LIMIT),
			new Expense(ExpenseType.Lunch, Constants.ExpenseLimits.LUNCH_EXPENSE_LIMIT),
			new Expense(ExpenseType.Dinner, Constants.ExpenseLimits.DINNER_EXPENSE_LIMIT)]
		);


		var writer = new StringWriter();
		report.PrintReportToWriter(expenses, writer);

		string result = writer.ToString();

		Assert.False(result.Contains("X")); // no Xes anywhere
	}

}