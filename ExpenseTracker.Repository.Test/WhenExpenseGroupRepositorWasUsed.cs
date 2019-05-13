using ExpenseTracker.Model.EF;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.Test
{
    [TestFixture, Category("UnitTest")]
    public sealed  class WhenExpenseGroupRepositorWasUsed
    {
        private IExpenseGroupRepository _expenseGroupRespository;
        private Mock<ExpenseTrackerContext> moqDbContext;

        [SetUp]
        public void SetUp()
        {
            moqDbContext = new Mock<ExpenseTrackerContext>();
            _expenseGroupRespository = new ExpenseGroupRepository(moqDbContext.Object);
           
           
        }


        [Test]
        public void WhenExpensegroupGetAllWasInvoked()
        {
            moqDbContext.Setup(x => x.age).Returns(new )
            var result = _expenseGroupRespository.GetAll();
            Assert.AreEqual(result.Count(), 1);
        }



    }
}
