using Banking.Dao;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking_Project.ApiServices
{
    public class TransactionService
    {
        private readonly SqlDataAccess _conn = new SqlDataAccess();
        public CreateTransfer Transfer(CreateTransfer reqModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Transfer", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("SenderAccNo", reqModel.SenderAccountNo);
                cmd.Parameters.AddWithValue("ReceiverAccNo", reqModel.ReceiverAccountNo);
                cmd.Parameters.AddWithValue("TransferAmount", reqModel.TransferAmount);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {

                    return new CreateTransfer()
                    {
                        //lstAccount = accountList,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString(),
                        }
                    };
                }
                return new CreateTransfer()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = "999",
                        RespDesp = dt.Rows[0][1].ToString(),
                        RespMessageType = Common.Message_ME//clolum 1
                    }
                };
            }
            catch (Exception e)
            {
                return new CreateTransfer()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = "999",
                        RespDesp = e.Message,
                        RespMessageType = Common.Message_ME//clolum 1
                    }
                };
            }
            //CommonMessageModel model = new CommonMessageModel();


        }

        public TopUp TopUp(TopUp reqModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TopUp", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("SenderAccNo", reqModel.SenderAccountNo);
                cmd.Parameters.AddWithValue("OperatorName", reqModel.OperatorName);
                cmd.Parameters.AddWithValue("TransferAmount", reqModel.TransferAmount);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    return new TopUp()
                    {
                        //lstAccount = accountList,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString(),
                        }
                    };
                }

                return new TopUp()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = "999",
                        RespDesp = dt.Rows[0][0].ToString(),
                        RespMessageType = Common.Message_ME//clolum 1
                    }
                };
            }
            catch (Exception e)
            {
                return new TopUp()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = "999",
                        RespDesp = e.Message,
                        RespMessageType = Common.Message_ME//clolum 1
                    }
                };
            }


        }
    }
}