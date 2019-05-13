using ExpenseTracker.Controllers;
using ExpenseTracker.Model.EF;
using ExpenseTracker.Models;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace ExpenseTracker.Test.Tests
{
    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenExpenseUserServicesAreExecutedWithExpenseTrackerControllers
    {

        private Mock<IExpenseGroupService> _groupServiceMock;
        private Mock<IExpenseUserService> _userServiceMock;
        private Mock<IExpenseItemService> _expenseItemServiceMock;
        private ExpenseTrackerController _SystemUnderTest;


        [SetUp]
        public void SetUp()
        {

            _groupServiceMock = new Mock<IExpenseGroupService>();
            _userServiceMock = new Mock<IExpenseUserService>();
            _expenseItemServiceMock = new Mock<IExpenseItemService>();

            _SystemUnderTest = new ExpenseTrackerController(_groupServiceMock.Object, _userServiceMock.Object, _expenseItemServiceMock.Object);
        }


        [Test]
        public void ExpenseGroupCreate()
        {
            var user = new ExpenseUserVM() { Name = "Test",Email = "ss@ss.com" };
            var result = (RedirectToRouteResult)_SystemUnderTest.CreateUser(user);

            Assert.AreEqual(true, _SystemUnderTest.ModelState.IsValid);
            _userServiceMock.Verify(m => m.Create(It.IsAny<ExpenseUser>()), Times.Once);
        }

        [Test]
        public void InvalidUserCreate()
        {
            var user = new ExpenseUserVM() { Name = "Test", Email = "ss@ss.com" };
            _SystemUnderTest.ModelState.AddModelError("Error", "Something went wrong");

            var result = (ViewResult)_SystemUnderTest.CreateUser(user);

            Assert.AreEqual(false, _SystemUnderTest.ModelState.IsValid);
            _userServiceMock.Verify(m => m.Create(It.IsAny<ExpenseUser>()), Times.Never);
            Assert.AreEqual("AddUser", result.ViewName);
        }


        [Test]
        public void ValidateUserCreate()
        {
            var user = new ExpenseUserVM() { Name = "Test" };
            _SystemUnderTest.ModelState.AddModelError("Error", "Email is required");

            var result = (ViewResult)_SystemUnderTest.CreateUser(user);

            Assert.AreEqual(false, _SystemUnderTest.ModelState.IsValid);
            _userServiceMock.Verify(m => m.Create(It.IsAny<ExpenseUser>()), Times.Never);
            Assert.AreEqual("AddUser", result.ViewName);
        }
    }
}
