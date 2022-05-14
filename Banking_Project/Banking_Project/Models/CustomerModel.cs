
using Banking.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Banking_Project.Models
{
    public class CustomerModel
    {
        public CommonMessageModel msg { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set;}
        [RegularExpression(@"^([\d]{1,2})\/([\w]{ 3}|[\w]{6})\((?:N|NAING)\)([\d]{6})$", ErrorMessage = "This is not valid NRC format")]
        public string NRC { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<Customer> lstCustomer { get; set; }
        public List<CustomerAccounts> lstAccount { get; set; }
        public List<TotalTransaction> lstTotalTransaction { get; set; } = new List<TotalTransaction>();

        public List<BarChart> lstbarchart { get; set; } = new List<BarChart>();
        public int lstDayRangeTotal { get { return lstbarchart == null ? 0 : lstbarchart.Count; } }
        public List<BarChartItem> lstItem { get; set; }

    }
    public class TotalTransactionModel
    {
        public CommonMessageModel msg { get; set; }
        public decimal TransactionAmount { get; set; }
        public List<TotalTransaction> lstTotalTransaction { get; set; }
    }
    public class TotalTransaction
    {
        public decimal TransactionAmount { get; set; }
    }
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string TotalCustomer { get; set; }
        [Required]
        [RegularExpression(@"^([\d]{1,2})\/([\w]{ 3}|[\w]{6})\((?:N|NAING)\)([\d]{6})$", ErrorMessage = "This is not valid NRC format")]
        public string NRC { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phoneno { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString ="{0:dd MM yyyy}")]
        public DateTime CreatedDate { get; set; }
    }
    public class AccountModel
    {
        public CommonMessageModel msg { get; set; }
        public int No { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public List<Account> lstAccount { get; set; }

    }
    public class Account
    {
        public int No { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        [Required]
        public decimal Amount { get; set; }
         [DisplayName("Date")]
        [DisplayFormat(DataFormatString ="{0:dd MM yyyy}")]
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }

    }

    public class CustomerAccounts
    {
        public string CustomerName { get; set; }
        public string NRC { get; set; }
        public string Phoneno { get; set; }
        public string AccountNo { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Account")]
        public string AccountType { get; set; }
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
    }

    public class TransactionHistoryModel
    {
        public CommonMessageModel msg { get; set; }
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string AccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Flash { get; set; }
        public string TrasactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<TransactionHistory> lstTransactionHistory { get; set; }
        public int lstTrasactionTotal { get { return lstTransactionHistory == null ? 0 : lstTransactionHistory.Count; } }

    }
    public class TransactionHistory
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string AccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Flash { get; set; }
        public string TrasactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class CreateTransfer
    {
        public CommonMessageModel msg { get; set; }
        public string SenderAccountNo { get; set; }
        public string ReceiverAccountNo { get; set; }
        public decimal TransferAmount { get; set; }
        //public DateTime TransactionDate { get; set; }
    }
    public class TopUpModel
    {
        public CommonMessageModel msg { get; set; }
        public string SenderAccountNo { get; set; }
        public string OperatorName { get; set; }
        public decimal TransferAmount { get; set; }
        //public DateTime TransactionDate { get; set; }
    }
    public class TopUp
    {
        public CommonMessageModel msg { get; set; }
        public string SenderAccountNo { get; set; }
        public string OperatorName { get; set; }
        public decimal TransferAmount { get; set; }
    }

    public class TransactionByCustomerModel
    {
        public CommonMessageModel msg { get; set; }
        public List<TransactionByCustomer> lstTransactionByCustomer { get; set; } = new List<TransactionByCustomer>();
        public int lstTransactionByCustomerTotal { get { return lstTransactionByCustomer == null ? 0 : lstTransactionByCustomer.Count; } }

    }
    public class TransactionByCustomer //class for customer inqeury
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SenderAccountNo { get; set; }
        public string ReceiverAccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDateStr { get { return TransactionDate.ToString("dd-MM-yyyy"); } }
        public DateTime TransactionDate { get; set; }

    }

    public class TopUpListModel
    {
        public CommonMessageModel msg { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<TopUpList> lstTopUpList { get; set; } = new List<TopUpList>();
    }

    public class TopUpList
    {
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }


    public class TransactionByDayRangeModel
    {
        public CommonMessageModel msg { get; set; }
        public List<TransactionByDayRange> lstDayRange { get; set; } = new List<TransactionByDayRange>();
        public int lstDayRangeTotal { get { return lstDayRange == null ? 0 : lstDayRange.Count; } }
    }
    public class TransactionByDayRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SenderAccountNo { get; set; }
        public string ReceiverAccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TranscationDateStr { get { return TransactionDate.ToString("dd-MM-yyyy"); } }
        public DateTime TransactionDate { get; set; }
    }

    //public class BarChartModel
    //{
    //    public CommonMessageModel msg { get; set; }
    //    public List<BarChart> lstbarchart { get; set; } = new List<BarChart>();
    //    public int lstDayRangeTotal { get { return lstbarchart == null ? 0 : lstbarchart.Count; } }
    //    public List<BarChartItem> lstItem { get; set; }
    //}
    public class BarChartItem
    {
        public string Name { get; set; }
        public int[] lst { get; set; }
    }
    public class BarChart
    {
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalAmount { get; set; }
    }


}