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
    public class TransactionServices
    {
        private readonly SqlDataAccess _conn = new SqlDataAccess();
        public TransactionHistoryModel SelectTransactions()
        {

            //TransactionModel model = new TransactionModel();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Transaction_log", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();

                DataTable dt = ds.Tables[0];
                var model = new TransactionHistoryModel();
                model.lstTransactionHistory = dt.AsEnumerable().Select(
                    row => new TransactionHistory()
                    {
                        Id = Convert.ToString(row["Id"]),
                        TransactionId = Convert.ToString(row["Transaction_Id"]),
                        AccountNo = Convert.ToString(row["AccountNo"]),
                        TransactionAmount = Convert.ToDecimal(row["TransactionAmount"]),
                        Flash = Convert.ToString(row["Flash"]),
                        TrasactionType = Convert.ToString(row["TransactionType"]),
                        TransactionDate = Convert.ToDateTime(row["TransactionDate"])
                    }).ToList();
                model.msg = new CommonMessageModel()
                {
                    RespCode = "000",
                    RespDesp = "Saving Successful!",
                    RespMessageType = Common.Message_MS
                };
                return model;
            }
            catch (Exception ex)
            {
                return new TransactionHistoryModel()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = Common.ExceptionCode,
                        RespDesp = ex.Message,
                        RespMessageType = Common.Message_ME
                    }
                };
            }

        }

        public List<TotalTransaction> TotalTransactionAmt()
        {

            //TransactionModel model = new TransactionModel();

            try
            {
                SqlCommand cmd = new SqlCommand("select SUM(TransactionAmount)as Total from Transaction_log", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                List<TotalTransaction> lst = ds.Tables[0].AsEnumerable().
                 Select(row => new TotalTransaction()
                 {
                     TransactionAmount = Convert.ToDecimal(row["Total"]),
                 }).ToList();
                return lst;
            }
            catch (Exception e)
            {
                var lis = new List<TotalTransaction>();
                return lis;
            }


        }

        public CustomerModel BarChart()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("BarChart", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("TransactionDate", transactionDate);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();

                DataTable dt = ds.Tables[0];
                var model = new CustomerModel();
                model.lstbarchart = dt.AsEnumerable().Select(
                    row => new BarChart()
                    {
                        Year = Convert.ToInt32(row["Year"]),
                        Month = Convert.ToInt32(row["Month"]),
                        TransactionType = Convert.ToString(row["TransactionType"]),
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"])
                    }).ToList();

                return model;
            }
            catch (Exception ex)
            {
                return new CustomerModel()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = Common.ExceptionCode,
                        RespDesp = ex.Message,
                        RespMessageType = Common.Message_ME
                    }
                };
            }

        }
    }
}