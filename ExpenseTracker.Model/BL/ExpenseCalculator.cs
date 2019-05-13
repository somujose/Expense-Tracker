using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Model.BL
{
    public sealed class ExpenseCalculator : IExpenseCalculator
    {
        public readonly List<ExpenseLineItem> _expenses;

        public int NumberOfPeople => GetNumberOfPeople();

        public decimal ToalExpense => _expenses.Sum(x => x.AmountPaid);

        public ExpenseCalculator(List<ExpenseLineItem> expenses)
        {
            _expenses = expenses;
        }


        public decimal AverageExpense => ToalExpense / NumberOfPeople;

        public List<ExpenseLineItem> TotalExpensePaidByIndividuals => GetExpensePaidPerPerson().ToList();



        private IEnumerable<ExpenseLineItem> GetExpensePaidPerPerson()
        {
            IEnumerable<ExpenseLineItem> results = from p in _expenses group p by p.PersonId into g
                                                   select new ExpenseLineItem(g.Key,g.FirstOrDefault().PersonName, g.Sum(m => m.AmountPaid));
            return results.OrderBy(x => x.AmountPaid);
        }

        private int GetNumberOfPeople()
        {
            return _expenses.GroupBy(x => x.PersonId).Count();
        }

        public List<string> CalculateAmountOwedByIndividuals()
        {
            var moneyOwesByindividual = new List<string>();
            var expensePaidPerPerson = TotalExpensePaidByIndividuals;

            var personsList = expensePaidPerPerson.Select(x => x.PersonName).ToArray();
            var amoutOweList = expensePaidPerPerson.Select(x => x.AmountPaid - AverageExpense).ToArray();

            var i = 0;
            var j = expensePaidPerPerson.Count() - 1;
            var debtAmount = decimal.Zero;

            while (i < j)
            {
                debtAmount = -Math.Min(amoutOweList[i], amoutOweList[j]);
                if (amoutOweList[j] - debtAmount < decimal.Zero)
                {
                    var negativeAmount = amoutOweList[j] - debtAmount;
                    debtAmount += negativeAmount;
                }

                amoutOweList[i] += debtAmount;
                amoutOweList[j] -= debtAmount;
                if (debtAmount != decimal.Zero)
                    moneyOwesByindividual.Add($"{personsList[i]} owes {personsList[j]} ${debtAmount}");

                if (amoutOweList[i] == decimal.Zero)
                    i++;

                if (amoutOweList[j] == decimal.Zero)
                    j--;
            }

            return moneyOwesByindividual;
        }
    }
}
