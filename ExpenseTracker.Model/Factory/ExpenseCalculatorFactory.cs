using System;
using System.Collections.Generic;
using ExpenseTracker.Model.BL;

namespace ExpenseTracker.Model.Factory
{
    public sealed class ExpenseCalculatorFactory : IExpenseCalculatorFactory
    {
        private readonly List<ExpenseLineItem> _expenses;
        public ExpenseCalculatorFactory(List<ExpenseLineItem> expenses)
        {
            _expenses = expenses;
        }
        public IExpenseCalculator CreateExpenseCalculator()
        {
            return new ExpenseCalculator(_expenses);
        }
    }
}
