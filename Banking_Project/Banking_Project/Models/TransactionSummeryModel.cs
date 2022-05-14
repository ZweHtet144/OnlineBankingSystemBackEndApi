using Banking.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking_Project.Models
{
    public class TransactionSummeryModel
    {
        public CommonMessageModel msg { get; set; }
        public List<TransactionSummery> lstTransactionSummery { get; set; } = new List<TransactionSummery>();
        public int lstTransactionByCustomerTotal { get { return lstTransactionSummery == null ? 0 : lstTransactionSummery.Count; } }

    }
    public class TransactionSummery
    {

        public string CustomerName { get; set; }
        public string Transaction_id { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Flash { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
