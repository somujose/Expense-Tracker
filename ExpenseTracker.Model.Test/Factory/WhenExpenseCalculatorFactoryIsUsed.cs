using ExpenseTracker.Model.BL;
using ExpenseTracker.Model.Factory;
using NUnit.Framework;
using System.Collections.Generic;


namespace ExpenseTracker.Model.Test.Factory
{
    [TestFixture, Category("UnitTest")]
    public sealed class WhenExpenseCalculatorFactoryIsUsed
    {

        private IExpenseCalculatorFactory _expenseCalculatorFactory;
        private List<ExpenseLineItem> _items = new List<ExpenseLineItem>();

        #region Setup
        [SetUp]
        public void SetUp()
        {
            var expenseItem = new ExpenseLineItem(1, "Somu", 234);
            _items.Add(expenseItem);
            _expenseCalculatorFactory = new ExpenseCalculatorFactory(_items);
        }

        #endregion

        #region Test
        [Test]
        public void ThatCreateMethodWillCreateInstanceOfExpenseCalculator()
        {
            
            var application = _expenseCalculatorFactory.CreateExpenseCalculator();
            Assert.That(application, Is.InstanceOf<IExpenseCalculator>());
        }
        #endregion
    }
}

