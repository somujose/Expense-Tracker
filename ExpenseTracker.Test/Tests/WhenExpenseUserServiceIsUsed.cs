using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Test.Tests
{
    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenExpenseUserServiceIsUsed
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IExpenseUserRepository> _expenseUserRepository;
        private ExpenseUserService _userServiceMock;
        const int userId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _expenseUserRepository = new Mock<IExpenseUserRepository>();

            _userServiceMock = new ExpenseUserService(_unitOfWork.Object, _expenseUserRepository.Object);
        }




        [Test]
        public void WhenCreateInvoked()
        {
            var expenseUser = new ExpenseUser() { Id = 1, DisplayName = "Test1", Email = "test1@test.com" };
            _expenseUserRepository.Setup(x => x.Add(expenseUser));
            _userServiceMock.Create(expenseUser);
            _expenseUserRepository.Verify(m => m.Add(It.IsAny<ExpenseUser>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenDeleteInvoked()
        {

            var expenseUser = new ExpenseUser() { Id = 1, DisplayName = "Test1", Email = "test1@test.com" };
            _expenseUserRepository.Setup(x => x.Delete(expenseUser));
            _userServiceMock.Delete(expenseUser);
            _expenseUserRepository.Verify(m => m.Delete(It.IsAny<ExpenseUser>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenUpdateInvoked()
        {

            var expenseUser = new ExpenseUser() { Id = 1, DisplayName = "Test1", Email = "test1@test.com" };
            _expenseUserRepository.Setup(x => x.Edit(expenseUser));
            _userServiceMock.Update(expenseUser);
            _expenseUserRepository.Verify(m => m.Edit(It.IsAny<ExpenseUser>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenGetAllIsInvoked()
        {
            var listExpenseUser = new List<ExpenseUser>() {
                new ExpenseUser() { Id = 1, DisplayName = "Test1",Email ="test1@test.com" },
                new ExpenseUser() { Id = 2, DisplayName = "Test2",Email ="test2@test.com" },
                new ExpenseUser() { Id = 3, DisplayName = "Test3",Email ="test3@test.com"}
            };
            _expenseUserRepository.Setup(x => x.GetAll()).Returns(listExpenseUser);
            var result = _userServiceMock.GetAll().ToList();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Test1", result[0].DisplayName);
            Assert.AreEqual("Test2", result[1].DisplayName);
            Assert.AreEqual("Test3", result[2].DisplayName);
            _expenseUserRepository.Verify(m => m.GetAll(), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Never);
        }

    }
}
