using Banking.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking_Project.ApiServices
{
    public class TestService
    {
        private readonly SqlDataAccess _DbAccess = new SqlDataAccess();
        public List<CustomerModel> GetCustomerInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CUSTOMERINFO ORDER BY ID", _DbAccess.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                cmd.CommandTimeout = 18000;
                _DbAccess.Connect().Close();
                List<CustomerModel> lstCustomer = ds.Tables[0].AsEnumerable().Select(row => new CustomerModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Address = row["Address"].ToString(),
                    PhoneNo = row["PhoneNo"].ToString()
                }).ToList();
                return lstCustomer;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public class CustomerModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string PhoneNo { get; set; }
            public List<CustomerModel> CustomerList { get; set; }
        }
    }
}