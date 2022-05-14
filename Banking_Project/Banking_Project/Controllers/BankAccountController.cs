using Banking_Project.Services;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Banking_Project.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly AccountService _accountService = new AccountService();
        // GET: BankAccount
        //public ActionResult Deposite(string id)
        //{
        //    Account model = new Account();
        //    model.CustomerId = id;
        //    return View(model);
        //}
       
        public ActionResult Index()
        {
            var account = _accountService.SelectAccounts();
            return View(account);
        }

        public ActionResult Create(string id)
        {
            Account model = new Account();
            model.CustomerId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Account model)
        {
            _accountService.CreateBankAccount(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Account model)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            return RedirectToAction("Index");
        }
    }
}