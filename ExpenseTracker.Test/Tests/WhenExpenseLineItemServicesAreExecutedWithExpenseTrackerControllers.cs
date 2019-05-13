using ExpenseTracker.Controllers;
using ExpenseTracker.Model.EF;
using ExpenseTracker.Model.Factory;
using ExpenseTracker.Models;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;

namespace ExpenseTracker.Test.Tests
{
    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenExpenseLineItemServicesAreExecutedWithExpenseTrackerControllers
    {
        private Mock<IExpenseGroupService> _groupServiceMock;
        private Mock<IExpenseUserService> _userServiceMock;
        private Mock<IExpenseItemService> _expenseItemServiceMock;
        private ExpenseTrackerController _SystemUnderTest;

        private List<ExpenseItem> listExpenseItems;



        [SetUp]
        public void SetUp()
        {

            _groupServiceMock = new Mock<IExpenseGroupService>();
            _userServiceMock = new Mock<IExpenseUserService>();
            _expenseItemServiceMock = new Mock<IExpenseItemService>();

            listExpenseItems = new List<ExpenseItem>() {
                new ExpenseItem() {Id = 1, Amount = 223,ExpenseGroupId = 1,UserId = 1,User = new ExpenseUser() {DisplayName = "Somu",Email ="ss@ss.com",Id = 1 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} },
                new ExpenseItem() {Id = 2, Amount = 50,ExpenseGroupId = 1,UserId = 1,User = new ExpenseUser() {DisplayName = "Somu",Email ="ss@ss.com",Id = 1 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} },
                new ExpenseItem() {Id = 3, Amount = 75,ExpenseGroupId = 1,UserId = 2,User = new ExpenseUser() {DisplayName = "Dipu",Email ="dd@ss.com",Id = 2 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} }
            };

            _SystemUnderTest = new ExpenseTrackerController(_groupServiceMock.Object, _userServiceMock.Object, _expenseItemServiceMock.Object);
        }


        [Test]
        public void ExpenseItemFindByGroupIdUsingDetailsFromController()
        {
            _expenseItemServiceMock.Setup(x => x.FindByIncludinguserDetails(1)).Returns(listExpenseItems);
            var _mockCalculatorFactory = new Mock<IExpenseCalculatorFactory>();
            _SystemUnderTest._calculatorfactory = _mockCalculatorFactory.Object;

        var result = (_SystemUnderTest.Details(1) as ViewResult).Model as ExpenseSummaryVM;


            Assert.AreEqual(listExpenseItems.GroupBy(x => x.UserId).Count(),result.AmountCurrentlyPaidByIndividuals.Count);
            Assert.AreEqual(1,result.AmountOwedByIndividuals.Count);
            Assert.AreEqual(listExpenseItems.Sum(x=>x.Amount), result.ToalExpense);
            Assert.AreEqual(listExpenseItems.Sum(x => x.Amount)/ result.AmountCurrentlyPaidByIndividuals.Count, result.AverageExpense);
            Assert.AreEqual(listExpenseItems.GroupBy(x => x.UserId).Count(), result.NumberOfPeople);
        }


        [Test]
        public void ExpenseItemCreate()
        {
            var item = new ExpenseItemVM() { Amount = 23,GroupId = 1, UserId = 1 };
            var result = (RedirectToRouteResult)_SystemUnderTest.CreateExpense(item);

            Assert.AreEqual(true, _SystemUnderTest.ModelState.IsValid);
            _expenseItemServiceMock.Verify(m => m.Create(It.IsAny<ExpenseItem>()), Times.Once);
        }


        [Test]
        public void InvalidExpenseCreate()
        {
            var item = new ExpenseItemVM() { Amount = 23, GroupId = 1};
            _SystemUnderTest.ModelState.AddModelError("Error", "No user selected");

            var result = (ViewResult)_SystemUnderTest.CreateExpense(item);

            Assert.AreEqual(false, _SystemUnderTest.ModelState.IsValid);
            _expenseItemServiceMock.Verify(m => m.Create(It.IsAny<ExpenseItem>()), Times.Never);
            Assert.AreEqual("AddExpense", result.ViewName);
        }


    }


    
}
