using Banking_Project.Models;
using Banking_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banking_Project.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionServices _TransactionService = new TransactionServices();
        // GET: Transaction
        public ActionResult Index()
        {
            var transactionHistory = _TransactionService.SelectTransactions();
            return View(transactionHistory);
        }

        public ActionResult BarChart()
        {
            var summary = _TransactionService.BarChart();
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
            summary.lstItem = lst;
            return View(summary);
        }
    }
}