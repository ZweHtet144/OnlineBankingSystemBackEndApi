using Banking.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking_Project.Models
{
    public class CustomerPasswordChangeModel
    {
        public CommonMessageModel msg { get; set; }
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CustomerPasswordChange> lstCustomerPasswordChange { get; set; }
    }
    public class CustomerPasswordChange
    {
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
