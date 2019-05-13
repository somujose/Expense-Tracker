using System;
using System.Collections.Generic;

namespace ExpenseTracker.Model.BL
{
    public interface IExpenseCalculator
    {
        int NumberOfPeople { get; }
        decimal ToalExpense { get; }
        decimal AverageExpense { get; }

        List<ExpenseLineItem> TotalExpensePaidByIndividuals { get; }

        List<string> CalculateAmountOwedByIndividuals();

       
    }
}
