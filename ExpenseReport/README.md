# ExpenseReport

License: CC-BY-SA (see source: https://github.com/christianhujer/expensereport)

The ExpenseReport legacy code refactoring kata in various languages.

This is an example of a piece of legacy code with lots of code smells.

The goal is to support the following new feature as best as you can:
* Add Lunch with an expense limit of 2000.

## Process

1. 📚 Read the code to understand what it does and how it works.
2. 🦨 Read the code and check for design and code smells. Make a list of all code and design smells that you find.
3. 🧑‍🔬 Analyze what you would have to change to implement the new requirement without refactoring the code.
4. 🧪 Write a characterization test. Expand your list of code and design smells. Add those smells that you missed earlier and discovered now because they made your life writing a test miserable.
5. 🔧 Refactor the code.
6. 🔧 Refactor the test.
7. 👼 Test-drive the new feature.

## Design Code Smells Discovered

1. Poor file naming (Class1.cs)
2. Poor coding construct grouping (all in 1 cs file)
3. Primitive obsession: ExpenseType enum
4. No encapsulation of Expense fields - will create Tell, Don't Ask violations
5. Static cling: Direct usage of Console.WriteLine for output
6. Static cling: DateTime.Now in PrintReport
7. Tell, Don't Ask violation line 28-31 (adding expenses of type meal)
8. Conditional over polymorphism (switch on expense type)
9. Magic strings
10. Magic numbers
11. Tell, Don't Ask when creating mealOverExpensesMarker

## Starting with Tests and a Console Runner

1. Updating projects to net8
2. Adding Program Main to run the report and view results
3. Adding 3 initial sample expenses showing all 3 expense types and a dinner over limit

## How would I implement adding Lunch without refactoring

1. Add LUNCH to the enum.
2. Update meal conditional on line 28
3. Add case to switch with Lunch expense name
4. Update ternary on line 47 with lunch limit of 2000
5. SHIP IT!

## Write a Characterization Test

1. My Program Main is a simple version of such a test. I can pipe the result to a file and diff it if necessary. The timestamp on line 1 may be a problem but one I can easily ignore (by ignoring that line when I compare).
2. Writing a basic characterization test in NUnit. It creates the class and calls PrintReport but doesn't even display the console output. It doesn't blow up, though, so that's something...

Problems with testability:

1. DateTime.Now coupling
2. Direct Console output

## Refactor the Code

1. Introducing a SmartEnum for ExpenseType that will hold name, isMeal, and maxExpense properties.
2. Introducing constants for expense limits
3. Using decimals for monetary values
4. Update Program Main with modified Expense type and additional required constructor arguments
5. Run the program - behavior is consistent.
6. Refactor ternary code to check if over limit:

```csharp
String mealOverExpensesMarker = expense.Amount > expense.Type.MaxExpense
    //expense.type == ExpenseType.DINNER && expense.amount > 5000 ||
    //expense.type == ExpenseType.BREAKFAST && expense.amount > 1000
    ? "X"
    : " ";
```

7. Replace total and mealtotal with new code
8. Remove commented code and verify everything runs
9. Replace DateTime.Now with TimeProvider
10. Replace Console usage with TextWriter and extract new method.
11. Refactor characterization tests for textwriter and timeprovider.

## Credits and License

See: https://github.com/christianhujer/expensereport
