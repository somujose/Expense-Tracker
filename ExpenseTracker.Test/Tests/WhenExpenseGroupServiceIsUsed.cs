using ExpenseTracker.Model.EF;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Test.Tests
{

    [TestFixture, NUnit.Framework.Category("UnitTest")]
    public sealed class WhenExpenseGroupServiceIsUsed
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IExpenseGroupRepository> _expenseGroupRepository;
        private ExpenseGroupService _groupServiceMock;
        const int groupId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _expenseGroupRepository = new Mock<IExpenseGroupRepository>();

            _groupServiceMock = new ExpenseGroupService(_unitOfWork.Object, _expenseGroupRepository.Object);
        }

        [Test]
        public void WhenGetByIdIsInvoked()
        {
            var expenseGroup = new ExpenseGroup() { Id = 1, Name = "Group 1" };
            _expenseGroupRepository.Setup(x => x.GetById(groupId)).Returns(expenseGroup);
            var result = _groupServiceMock.GetById(groupId);
            Assert.That(result, Is.InstanceOf<ExpenseGroup>());
            Assert.AreEqual(expenseGroup.Name, result.Name);
            Assert.AreEqual(expenseGroup.Id, result.Id);
            _unitOfWork.Verify(m => m.Commit(), Times.Never);
        }


        [Test]
        public void WhenCreateInvoked()
        {
            var expenseGroup = new ExpenseGroup() { Name = "Group 1" };
            _expenseGroupRepository.Setup(x => x.Add(expenseGroup)) ;
            _groupServiceMock.Create(expenseGroup);
            _expenseGroupRepository.Verify(m => m.Add(It.IsAny<ExpenseGroup>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenDeleteInvoked()
        {
            var expenseGroup = new ExpenseGroup() { Name = "Group 1" };
            _expenseGroupRepository.Setup(x => x.Delete(expenseGroup));
            _groupServiceMock.Delete(expenseGroup);
            _expenseGroupRepository.Verify(m => m.Delete(It.IsAny<ExpenseGroup>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenUpdateInvoked()
        {
            var expenseGroup = new ExpenseGroup() { Name = "Group 1" };
            _expenseGroupRepository.Setup(x => x.Edit(expenseGroup));
            _groupServiceMock.Update(expenseGroup);
            _expenseGroupRepository.Verify(m => m.Edit(It.IsAny<ExpenseGroup>()), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void WhenGetAllIsInvoked()
        {
            var listExpenseGroup = new List<ExpenseGroup>() {
                new ExpenseGroup() { Id = 1, Name = "Group 1" },
                new ExpenseGroup() { Id = 2, Name = "Group 2" },
                new ExpenseGroup() { Id = 3, Name = "Group 3" }
            };
            _expenseGroupRepository.Setup(x => x.GetAll()).Returns(listExpenseGroup);
            var result = _groupServiceMock.GetAll().ToList();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Group 1", result[0].Name);
            Assert.AreEqual("Group 2", result[1].Name);
            Assert.AreEqual("Group 3", result[2].Name);
            _expenseGroupRepository.Verify(m => m.GetAll(), Times.Once);
            _unitOfWork.Verify(m => m.Commit(), Times.Never);
        }
    }
}
