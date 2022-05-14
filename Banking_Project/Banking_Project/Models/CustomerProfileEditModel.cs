using Banking.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking_Project.Models
{
    public class CustomerProfileEditModel
    {
        public CommonMessageModel msg { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public int Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<CustomerProfileEdit> lstCustomerProfileEdit { get; set; }
    }

    public class CustomerProfileEdit
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Version { get; set; }
    }
}