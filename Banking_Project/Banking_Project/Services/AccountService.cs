using Banking.Dao;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking_Project.Services
{
    public class AccountService
    {
        private readonly SqlDataAccess _conn = new SqlDataAccess();

        public void CreateBankAccount(Account reqModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CreateAccount", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("AccountNo", reqModel.AccountNo);
                cmd.Parameters.AddWithValue("AccountType", reqModel.AccountType);
                cmd.Parameters.AddWithValue("Amount", reqModel.Amount);
                cmd.Parameters.AddWithValue("CustomerId", reqModel.CustomerId);
                cmd.Parameters.AddWithValue("Version", 1);
                cmd.Parameters.AddWithValue("IsDeleted", "False");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
            }
            catch (Exception e)
            {

            }


        }

        public void Deposite(Account reqModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Deposit", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Amount", reqModel.Amount);
                cmd.Parameters.AddWithValue("AccountNo", reqModel.AccountNo);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
            }
            catch (Exception e)
            {

            }

        }

        public List<Account> SelectAccounts()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("select * from AccountInfo;", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();

                List<Account> lst = ds.Tables[0].AsEnumerable().
                       Select(row => new Account()
                       {
                           AccountNo = Convert.ToString(row["AccountNo"]),
                           AccountType = Convert.ToString(row["AccountType"]),
                           Amount = Convert.ToDecimal(row["Amount"]),
                           CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                           CustomerId = Convert.ToString(row["CustomerId"])
                       }).ToList();
                return lst;
            }
            catch (Exception e)
            {
                var lis = new List<Account>();
                return lis;
            }

        }
    }
}