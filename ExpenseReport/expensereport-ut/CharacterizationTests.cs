using NUnit.Framework;
using expensereport_csharp;
using System;
using System.IO;
using Microsoft.Extensions.Time.Testing;

namespace Tests
{
	public class CharacterizationTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void CharacterizationTest1()
		{
			var timeProvider = new FakeTimeProvider();
			timeProvider.SetUtcNow(new DateTimeOffset(2024, 1, 6, 15, 43, 0, TimeSpan.FromHours(-5)));

			var report = new ExpenseReport(timeProvider);

			var expense = new Expense(ExpenseType2.Dinner, 50);

			var writer = new StringWriter();
			report.PrintReportToWriter([expense], writer);

			string result = writer.ToString();

			Assert.True(result.Contains("1/6/2024 3:43:00 PM"));
			Assert.True(result.Contains("Dinner\t50"));
		}
	}
}
