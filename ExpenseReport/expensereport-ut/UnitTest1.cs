using NUnit.Framework;
using expensereport_csharp;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CharacterizationTest1()
        {
            var report = new ExpenseReport(TimeProvider.System);

            var expense = new Expense(ExpenseType2.Dinner, 50) { type = ExpenseType.DINNER, amount = 50 };

            report.PrintReport([expense]);

            Assert.Pass();
        }
    }
}