using Ardalis.SmartEnum;

namespace expensereport_csharp;

public class ExpenseType : SmartEnum<ExpenseType>
{
    public static readonly ExpenseType Dinner = new ExpenseType(nameof(Dinner), 0, Constants.ExpenseLimits.DINNER_EXPENSE_LIMIT, true);
    public static readonly ExpenseType Breakfast = new ExpenseType(nameof(Breakfast), 1, Constants.ExpenseLimits.BREAKFAST_EXPENSE_LIMIT, true);
    public static readonly ExpenseType CarRental = new ExpenseType("Car Rental", 2, 0, false);
    public static readonly ExpenseType Lunch = new ExpenseType(nameof(Lunch), 3, Constants.ExpenseLimits.LUNCH_EXPENSE_LIMIT, true);

    public decimal MaxExpense { get; set; }
    public bool IsMeal { get; set; }

    private ExpenseType(string name, int value, decimal maxExpense, bool isMeal) : base(name, value)
    {
        MaxExpense = maxExpense;
        IsMeal = isMeal;
    }
}
