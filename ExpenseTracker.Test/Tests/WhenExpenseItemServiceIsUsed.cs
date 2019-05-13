using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace ExpenseTracker.Test.Tests
{
    [TestFixture, Category("UnitTest")]
    public sealed class WhenExpenseItemServiceIsUsed
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IExpenseItemRepository> _expenseItemRepository;
        private ExpenseItemService _itemServiceMock;
        const int groupId = 1;


        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _expenseItemRepository = new Mock<IExpenseItemRepository>();

            _itemServiceMock = new ExpenseItemService(_unitOfWork.Object, _expenseItemRepository.Object);
        }
        [Test]
        public void WhenCreateInvoked()
        {
            var expenseItem = new ExpenseItem() { Amount = 223, ExpenseGroupId = 1, UserId = 1, User = new ExpenseUser() { DisplayName = "Somu", Email = "ss@ss.com", Id = 1 }, Group = new ExpenseGroup { Id = 1, Name = "Test" } };
            _expenseItemRepository.Setup(x => x.Add(expenseItem));
            _itemServiceMock.Create(expenseItem);
            _expenseItemRepository.Verify(m => m.Add(It.IsAny<ExpenseItem>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenDeleteInvoked()
        {
            var expenseItem = new ExpenseItem() { Id = 1, Amount = 223, ExpenseGroupId = 1, UserId = 1, User = new ExpenseUser() { DisplayName = "Somu", Email = "ss@ss.com", Id = 1 }, Group = new ExpenseGroup { Id = 1, Name = "Test" } };
            _expenseItemRepository.Setup(x => x.Delete(expenseItem));
            _itemServiceMock.Delete(expenseItem);
            _expenseItemRepository.Verify(m => m.Delete(It.IsAny<ExpenseItem>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenUpdateInvoked()
        {

            var expenseItem = new ExpenseItem() { Id = 1, Amount = 223, ExpenseGroupId = 1, UserId = 1, User = new ExpenseUser() { DisplayName = "Somu", Email = "ss@ss.com", Id = 1 }, Group = new ExpenseGroup { Id = 1, Name = "Test" } };
            _expenseItemRepository.Setup(x => x.Edit(expenseItem));
            _itemServiceMock.Update(expenseItem);
            _expenseItemRepository.Verify(m => m.Edit(It.IsAny<ExpenseItem>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenFindByWithGroupIdInvoked()
        {
            var listExpenseUser  = new List<ExpenseItem>() {
                new ExpenseItem() {Id = 1, Amount = 223,ExpenseGroupId = 1,UserId = 1,User = new ExpenseUser() {DisplayName = "Somu",Email ="ss@ss.com",Id = 1 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} },
                new ExpenseItem() {Id = 2, Amount = 50,ExpenseGroupId = 1,UserId = 1,User = new ExpenseUser() {DisplayName = "Somu",Email ="ss@ss.com",Id = 1 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} },
                new ExpenseItem() {Id = 3, Amount = 75,ExpenseGroupId = 1,UserId = 2,User = new ExpenseUser() {DisplayName = "Dipu",Email ="dd@ss.com",Id = 2 },Group = new ExpenseGroup{ Id = 1,Name = "Test"} }
            };
            _expenseItemRepository.Setup(x => x.FindByIncludingUserDetails(groupId)).Returns(listExpenseUser);
            var result = _itemServiceMock.FindByIncludinguserDetails(groupId).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(223, result[0].Amount);
            Assert.AreEqual(50, result[1].Amount);
            Assert.AreEqual(75, result[2].Amount);

            _expenseItemRepository.Verify(m => m.FindByIncludingUserDetails(groupId), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Never);
        }
    }
}
