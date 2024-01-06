namespace expensereport_csharp;

public class Expense(ExpenseType expenseType, decimal amount)
{
    public ExpenseType Type { get; set; } = expenseType;
    public decimal Amount { get; set; } = amount;
}
