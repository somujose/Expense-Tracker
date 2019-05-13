using ExpenseTracker.Model.BL;
using ExpenseTracker.Model.Factory;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Model.Test.BLL
{
    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenExpenseCalculatorIsUsed
    {
        private Mock<IExpenseCalculatorFactory> _moqExpenseCalculatorFactory;
        private List<ExpenseLineItem> _items = new List<ExpenseLineItem>();


        #region setUp
        [SetUp]
        public void SetUp()
        {

            var expenseItem1 = new ExpenseLineItem(1, "Somu", 234);
            var expenseItem2 = new ExpenseLineItem(2, "Somu1", 455);
            var expenseItem3 = new ExpenseLineItem(1, "Somu", 343);

            _items.Add(expenseItem1);
            _items.Add(expenseItem2);
            _items.Add(expenseItem3);
            _moqExpenseCalculatorFactory = new Mock<IExpenseCalculatorFactory>();


            _moqExpenseCalculatorFactory.Setup(x => x.CreateExpenseCalculator()).Returns(new ExpenseCalculator(_items));
            

        }
        #endregion

        [Test]
        public void CreateExpenseCalculator()
        {
            var calculator = _moqExpenseCalculatorFactory.Object.CreateExpenseCalculator();
            Assert.AreEqual(calculator.NumberOfPeople, _items.GroupBy(x=>x.PersonId).Count());
            Assert.That(calculator.ToalExpense, Is.EqualTo(_items.Sum(x => x.AmountPaid)));
            Assert.AreNotEqual(calculator.NumberOfPeople, _items.Count());
        }

    }
}
