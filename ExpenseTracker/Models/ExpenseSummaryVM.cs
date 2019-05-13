using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class ExpenseSummaryVM
    {

        public int GroupId { get; set; }

        [Display(Name = "Amount Owed by person")]
        public List<string> AmountOwedByIndividuals { get; set; }

        [Display(Name = "Amount Spend Currently")]
        public List<string> AmountCurrentlyPaidByIndividuals { get; set; }

        [Display(Name = "Number Of People")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "Avergage Expense Per Person"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal AverageExpense { get; set; }

        [Display(Name = "Total Expense"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ToalExpense { get; set; }
    }
}