using NUnit.Framework;
using expensereport_csharp;
using System;
using System.IO;

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
		decimal expenseAmount = 5001m;

		var expense = new Expense(ExpenseType2.Dinner, expenseAmount) { type = ExpenseType.DINNER, amount = (int)expenseAmount };

		var writer = new StringWriter();
		report.PrintReportToWriter([expense], writer);

		string result = writer.ToString();

		Assert.True(result.Contains($"Dinner\t{expenseAmount}\tX"));
	}
}