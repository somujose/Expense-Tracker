using ExpenseTracker.Model.BL;
using ExpenseTracker.Model.EF;
using ExpenseTracker.Model.Factory;
using ExpenseTracker.Models;
using ExpenseTracker.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseTrackerController : Controller
    {

        private readonly IExpenseGroupService _groupService;
        private readonly IExpenseUserService _userService;
        private readonly IExpenseItemService _expenseItemService;
        public IExpenseCalculatorFactory _calculatorfactory;


        public ExpenseTrackerController(
           IExpenseGroupService groupService,
            IExpenseUserService userService,
            IExpenseItemService expenseItemService)
        {
            _groupService = groupService;
            _userService = userService;
            _expenseItemService = expenseItemService;
        }
        // GET: ExpenseTracker
        public ActionResult Index()
        {
            var groups = _groupService.GetAll().ToList().Select(x => new ExpenseGroupVM { Id = x.Id, Name = x.Name, HasExpenses = _expenseItemService.HasExpenseAdded(x.Id) }); ;
            return View(groups);
        }


        public ActionResult Details(int GroupId)
        {

            var expenseLineItems = _expenseItemService.FindByIncludinguserDetails(GroupId).ToList().Select(GetExpeneLineItem).ToList();
            _calculatorfactory = new ExpenseCalculatorFactory(expenseLineItems);
            var calculator = _calculatorfactory.CreateExpenseCalculator();

            var VM = new ExpenseSummaryVM
            {
                GroupId = GroupId,
                AmountOwedByIndividuals = calculator.CalculateAmountOwedByIndividuals(),
                AverageExpense = calculator.AverageExpense,
                ToalExpense = calculator.ToalExpense,
                NumberOfPeople = calculator.NumberOfPeople,
                AmountCurrentlyPaidByIndividuals = AmountCurrentlyPaidByInduviduals(calculator.TotalExpensePaidByIndividuals)

            };

            return View(VM);
        }

        public ActionResult AddExpense(int GroupId)
        {
            var viewModel = new ExpenseItemVM
            {
                GroupId = GroupId,
                GroupName = _groupService.GetById(GroupId).Name,
                UserList = GetAllUsers()
            };

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateExpense(ExpenseItemVM data)
        {
            if (!ModelState.IsValid)
            {
                return View("AddExpense", data);
            }

            var expenseItem = new ExpenseItem
            {
                Amount = data.Amount,
                UserId = data.UserId,
                ExpenseGroupId = data.GroupId
            };
            _expenseItemService.Create(expenseItem);

            return RedirectToAction("Details", new { GroupId = data.GroupId });
        }


        public ActionResult AddGroup()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ExpenseGroupVM data)
        {
            if (!ModelState.IsValid)
            {
                return View("AddGroup", data);
            }

            var groupItem = new ExpenseGroup
            {
                Name = data.Name
            };
            _groupService.Create(groupItem);
            return RedirectToAction("Index");
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CreateUser(ExpenseUserVM data)
        {
            if (!ModelState.IsValid)
            {
                return View("AddUser", data);
            }

            var userItem = new ExpenseUser
            {
                DisplayName = data.Name,
                Email = data.Email
            };

            _userService.Create(userItem);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult IsAlreadyRegistered(string Email)
        {
            var emailExists =_userService.GetAll().ToList().Any(x => x.Email.ToLower().Trim() == Email.ToLower().Trim());
            return Json(!emailExists);

        }


        [NonAction]
        private ExpenseLineItem GetExpeneLineItem(ExpenseItem item)
        {
            return new ExpenseLineItem(item.UserId ?? 0, item.User.DisplayName, item.Amount);
        }

        [NonAction]
        private List<string> AmountCurrentlyPaidByInduviduals(List<ExpenseLineItem> items)
        {
            var amountPaid = new List<string>();
            foreach (var item in items)
                amountPaid.Add($"{item.PersonName} spents ${item.AmountPaid} ");

            return amountPaid;
        }
        [NonAction]
        private List<SelectListItem> GetAllUsers()
        {
            var users = _userService.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.DisplayName,
                Value = x.Id.ToString(),
                Selected = false
            });

            return users.ToList(); ;
        }

    }
}