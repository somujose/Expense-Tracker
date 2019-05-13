using ExpenseTracker.Model.BL;

namespace ExpenseTracker.Model.Factory
{
    public interface IExpenseCalculatorFactory
    {
        IExpenseCalculator CreateExpenseCalculator();
    }
}
