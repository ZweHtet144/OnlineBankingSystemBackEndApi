using Banking_Project.Services;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banking_Project.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private readonly CustomerService _customerService = new CustomerService();
        private readonly AccountService _accountService = new AccountService();
        private readonly TransactionServices _transactionService = new TransactionServices();

        public ActionResult Index()
        {
            var customer = _customerService.SelectCustomers();
            return View(customer);
        }
        public ActionResult CustomerAccounts()
        {
            var customeraccounts = _customerService.CustomerAccounts();
            return View(customeraccounts);
        }
        public ActionResult Create()
        {
            Customer model = new Customer();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Customer model)
        {

            if (!isValidNRC(model.NRC))
            {
                ViewData["Message"] = "Invalid NRC";
                return View();
            }
            _customerService.CreateCustomer(model);
            return RedirectToAction("Index");
        }

        public ActionResult CreateBankAccount(string id)
        {
            Account model = new Account();
            model.CustomerId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateBankAccount(Account model)
        {
            _accountService.CreateBankAccount(model);
            return RedirectToAction("customeraccounts");
        }

        public ActionResult Deposite(string id,string AccountType)
        {
            Account model = new Account();
            model.AccountNo = id;
            model.AccountType = AccountType;
            return View(model);
        }

        [HttpPost]
        public ActionResult Deposite(Account model)
        {
            _accountService.Deposite(model);
            return RedirectToAction("CustomerAccounts");
        }

        public ActionResult Edit(string id)
        {
            var model = _customerService.CustomerWithId(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            _customerService.Update(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string CustomerId)
        {
            var customer = _customerService.Delete(CustomerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AccountDelete(string id)
        {
            CustomerAccounts model = new CustomerAccounts();
            model.AccountNo = id;
            _customerService.AccountDelete(id);
            return RedirectToAction("CustomerAccounts");
        }

        public ActionResult TransactionByCustomerId()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TransactionByCustomerId(string CustomerId)
        {
             var transactions = _customerService.TransactionByCustomerId(CustomerId);
            return Json(transactions, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Dashboard()
        {
            var lstCustomer = _customerService.SelectCustomers();
            var lstAccount = _customerService.CustomerAccounts();
            var lstTotalTransaction = _transactionService.TotalTransactionAmt();
            var summary = _transactionService.BarChart();
            string[] str = { "Transfer", "TopUp", "Deposit" };
            List<BarChartItem> lst = new List<Models.BarChartItem>();
            for (int i = 0; i < 3; i++)
            {
                BarChartItem item = new Models.BarChartItem();
                item.Name = str[i];
                int[] arr = new int[12];
                for (int j = 0; j < 12; j++)
                {
                    arr[j] = Convert.ToInt32(summary.lstbarchart.
                        Where(x => x.Month == j + 1 && x.TransactionType.ToLower().
                        Equals(str[i].ToLower())).ToList().Sum(y => y.TotalAmount));
                }
                item.lst = arr;
                lst.Add(item);
            }
            CustomerModel model = new CustomerModel()
            {
                lstAccount = lstAccount,
                lstCustomer = lstCustomer,
                lstTotalTransaction = lstTotalTransaction,
                lstItem = lst
            };
            return View(model);
        }


        public ActionResult TransactionsByDayRange()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TransactionsByDayRange(DateTime StartDate, DateTime EndDate)
        {
            var transactions = _customerService.TransactionByStartDateEndDate(StartDate, EndDate);
            return Json(transactions, JsonRequestBehavior.AllowGet);
        }
        public static bool isValidNRC(string inputNRC)
        {
            string strRegex = @"^([\d]{1,2})\/([\w]{ 3}|[\w]{6})\((?:N|NAING)\)([\d]{6})$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(inputNRC))
                return (true);
            else
                return (false);
        }
       
    }
}
