﻿using System;
using System.Collections.Generic;
using Ardalis.SmartEnum;
using System.Linq;

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

public class ExpenseReport
{
    public void PrintReport(List<Expense> expenses)
    {
        //int total = 0;
        //int mealExpenses = 0;
        decimal mealExpenses2 = expenses.Where(exp => exp.Type.IsMeal).Sum(exp => exp.Amount);
        decimal total2 = expenses.Sum(exp => exp.Amount);

        Console.WriteLine("Expenses " + DateTime.Now);

        foreach (Expense expense in expenses)
        {
            //if (expense.type == ExpenseType.DINNER || expense.type == ExpenseType.BREAKFAST)
            //{
            //    mealExpenses += expense.amount;
            //}

            String expenseName = "";
            switch (expense.type)
            {
                case ExpenseType.DINNER:
                    expenseName = "Dinner";
                    break;
                case ExpenseType.BREAKFAST:
                    expenseName = "Breakfast";
                    break;
                case ExpenseType.CAR_RENTAL:
                    expenseName = "Car Rental";
                    break;
            }

            String mealOverExpensesMarker = expense.Amount > expense.Type.MaxExpense
                //expense.type == ExpenseType.DINNER && expense.amount > 5000 ||
                //expense.type == ExpenseType.BREAKFAST && expense.amount > 1000
                    ? "X"
                    : " ";

            Console.WriteLine(expenseName + "\t" + expense.amount + "\t" + mealOverExpensesMarker);

            total += expense.amount;
        }

        //Console.WriteLine("Meal expenses: " + mealExpenses);
        //Console.WriteLine("Total expenses: " + total);

        Console.WriteLine("Meal expenses2: " + mealExpenses2);
        Console.WriteLine("Total expenses2: " + total2);
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

        var report = new ExpenseReport();
        report.PrintReport(expenses);
    }
}