using ExpenseTracker.Controllers;
using ExpenseTracker.Model.EF;
using ExpenseTracker.Models;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTracker.Test.ServiceTests
{
    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenServicesAreExecutedWithControllers
    {
        private Mock<IExpenseGroupService> _groupServiceMock;
        private Mock<IExpenseUserService> _userServiceMock;
        private Mock<IExpenseItemService> _expenseItemServiceMock;
        private ExpenseTrackerController _SystemUnderTest;
        private List<ExpenseGroup> listExpenseGroup;

        [SetUp]
        public void SetUp()
        {
           
            _groupServiceMock = new Mock<IExpenseGroupService>();
            _userServiceMock = new Mock<IExpenseUserService>();
            _expenseItemServiceMock = new Mock<IExpenseItemService>();

            listExpenseGroup  = new List<ExpenseGroup>() {
                new ExpenseGroup() { Id = 1, Name = "Group 1" },
                new ExpenseGroup() { Id = 2, Name = "Group 2" },
                new ExpenseGroup() { Id = 3, Name = "Group 3" }
            };

            _SystemUnderTest = new ExpenseTrackerController(_groupServiceMock.Object, _userServiceMock.Object, _expenseItemServiceMock.Object);
        }

        [Test]
        public void ExpenseGroupGetAll()
        {
            _groupServiceMock.Setup(x => x.GetAll()).Returns(listExpenseGroup);
            _expenseItemServiceMock.Setup(x => x.HasExpenseAdded(listExpenseGroup[0].Id)).Returns(true);
            _expenseItemServiceMock.Setup(x => x.HasExpenseAdded(listExpenseGroup[1].Id)).Returns(false);
            _expenseItemServiceMock.Setup(x => x.HasExpenseAdded(listExpenseGroup[2].Id)).Returns(true);

            var result = (_SystemUnderTest.Index() as ViewResult).Model as IEnumerable<ExpenseGroupVM>;

            var groups = result.ToList();

            Assert.AreEqual(3, groups.Count());
            Assert.AreEqual("Group 1", groups[0].Name);
            Assert.AreEqual("Group 2", groups[1].Name);
            Assert.AreEqual("Group 3", groups[2].Name);
        }


        [Test]
        public void ExpenseGroupCreate()
        {
            ExpenseGroupVM ap = new ExpenseGroupVM() { Name = "Test" };
            var result = (RedirectToRouteResult)_SystemUnderTest.CreateGroup(ap);

            //Assert
            Assert.AreEqual(true, _SystemUnderTest.ModelState.IsValid);
            _groupServiceMock.Verify(m => m.Create(It.IsAny<ExpenseGroup>()), Times.Once);
        }


        [Test]
        public void InvalidGroupCreate()
        {
            //Arrange
            ExpenseGroupVM group = new ExpenseGroupVM() { Name = "Test" };
            _SystemUnderTest.ModelState.AddModelError("Error", "Something went wrong");
            
            //Act
            var result = (ViewResult)_SystemUnderTest.CreateGroup(group);

            //Assert
            Assert.AreEqual(false, _SystemUnderTest.ModelState.IsValid);
            _groupServiceMock.Verify(m => m.Create(It.IsAny<ExpenseGroup>()), Times.Never);
            Assert.AreEqual("AddGroup", result.ViewName);
        }

    }
}
