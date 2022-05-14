using Banking.Dao;
using Banking_Project.Dao;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking_Project.ApiServices
{
    public class CustomerApiServices
    {
        private readonly SqlDataAccess _context = new SqlDataAccess();
        #region CustomerLogin Api
        public CustomerModel Login(CustomerModel viewModel)
        {
            List<Customer> CustomerList = null;
            DataTable dt = null;

            string password = viewModel.Password;
            string email = viewModel.Email;
            try
            {
                SqlCommand ecmd = new SqlCommand("GetCustomerByEamil", _context.Connect());
                ecmd.CommandType = CommandType.StoredProcedure;
                ecmd.Parameters.AddWithValue("@Email", email);
                SqlDataAdapter eadpt = new SqlDataAdapter(ecmd);
                DataSet eds = new DataSet();
                eadpt.Fill(eds);
                _context.Connect().Close();

                DataTable edt = eds.Tables[0];
                if (edt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {

                    List<Customer> eCustomerList = eds.Tables[1].AsEnumerable().Select(row => new Customer()
                    {

                        Password = Convert.ToString(row["Password"]),

                    }).ToList();


                    AesReal aes = new AesReal();
                    string descryptpassword = aes.DecryptString(eCustomerList[0].Password.ToString());
                    if (password == descryptpassword)
                    {
                        string enpass = aes.EnryptString(password);
                        SqlCommand cmd = new SqlCommand("CustomerLogin", _context.Connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                        cmd.Parameters.AddWithValue("@Password", enpass);
                        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);
                        _context.Connect().Close();

                        dt = ds.Tables[0];
                        if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))

                        {

                            CustomerList = ds.Tables[1].AsEnumerable().Select(row => new Customer()
                            {
                                CustomerId = Convert.ToString(row["CustomerId"]),
                                CustomerName = Convert.ToString(row["CustomerName"]),
                                Email = Convert.ToString(row["Email"]),
                                Phoneno = Convert.ToString(row["PhoneNo"]),
                                NRC = Convert.ToString(row["NRC"]),
                                Address = Convert.ToString(row["Address"]),
                                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                                Password = Convert.ToString(row["Password"]),
                            }).ToList();

                        }
                        return new CustomerModel()
                        {
                            lstCustomer = CustomerList,
                            msg = new CommonMessageModel()
                            {
                                RespCode = dt.Rows[0][0].ToString(),
                                RespDesp = dt.Rows[0][1].ToString(),
                                RespMessageType = dt.Rows[0][2].ToString(),
                            }
                        };

                    }
                    else
                    {
                        return new CustomerModel()
                        {
                            msg = new CommonMessageModel()
                            {
                                RespCode = "999",
                                RespDesp = edt.Rows[0][0].ToString(),
                                RespMessageType = Common.Message_ME//clolum 1
                            }
                        };
                    }

                }

                else
                {
                    return new CustomerModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = "999",
                            RespDesp = edt.Rows[0][0].ToString(),//clolum 1
                            RespMessageType = Common.Message_ME
                        }
                    };
                }

            }
            catch (Exception e)
            {
                return new CustomerModel()
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
        #endregion CustomerLogin Api

        #region CustomerAccountInfos
        public AccountModel AccountByCustomerId(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AccountByCustomerId", _context.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", id);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                _context.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    List<Account> accountList = ds.Tables[1].AsEnumerable().Select(row => new Account()
                    {
                        AccountNo = Convert.ToString(row["AccountNo"]),
                        AccountType = Convert.ToString(row["AccountType"]),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        CustomerId = Convert.ToString(row["CustomerId"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    }).ToList();

                    return new AccountModel()
                    {
                        lstAccount = accountList,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString(),
                        }
                    };
                }
                return new AccountModel()
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
                return new AccountModel()
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
        #endregion CustomerAccountInfos

        #region CustomerProfileEdit
        public CustomerProfileEditModel PostCustomerProfileEdit(CustomerProfileEditModel viewModel)
        {
            List<CustomerProfileEdit> CustomerList = null;
            DateTime now = DateTime.Now;
            SqlCommand cmd = new SqlCommand("CustomerProfileEdit", _context.Connect());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", viewModel.CustomerId);
            cmd.Parameters.AddWithValue("@CustomerName", viewModel.CustomerName);
            cmd.Parameters.AddWithValue("@Email", viewModel.Email);
            cmd.Parameters.AddWithValue("@Address", viewModel.Address);
            cmd.Parameters.AddWithValue("@PhoneNo", viewModel.PhoneNo);
            cmd.Parameters.AddWithValue("@UpdatedDate", now);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            _context.Connect().Close();
            DataTable dt = ds.Tables[0];
            if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
            {
                List<CustomerProfileEdit> CustomerProfileList = ds.Tables[1].AsEnumerable().Select(row => new CustomerProfileEdit()
                {
                    CustomerName = Convert.ToString(row["CustomerName"]),
                    CustomerId = Convert.ToString(row["CustomerId"]),
                    Email = Convert.ToString(row["Email"]),
                    Address = Convert.ToString(row["Address"]),
                    PhoneNo = Convert.ToString(row["PhoneNo"]),
                    UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                    NRC = Convert.ToString(row["NRC"]),

                }).ToList();

                return new CustomerProfileEditModel()
                {
                    lstCustomerProfileEdit = CustomerProfileList,
                    msg = new CommonMessageModel()
                    {
                        RespCode = dt.Rows[0][0].ToString(),
                        RespDesp = dt.Rows[0][1].ToString(),
                        RespMessageType = dt.Rows[0][2].ToString(),
                    }
                };
            }
            return new CustomerProfileEditModel()
            {
                msg = new CommonMessageModel()
                {
                    RespCode = "999",
                    RespDesp = dt.Rows[0][0].ToString(),
                    RespMessageType = Common.Message_ME//clolum 1
                }
            };

        }
        #endregion

        #region PasswordEdit
        public CustomerPasswordChangeModel PostCustomerPasswordEdit(CustomerPasswordChangeModel viewModel)
        {
            List<CustomerPasswordChange> CustomerList = null;
            DataTable dt = null;

            string oldPassword = viewModel.OldPassword;
            string newPassword = viewModel.NewPassword;

            try
            {
                SqlCommand ecmd = new SqlCommand("GetCustomerByEamil", _context.Connect());
                ecmd.CommandType = CommandType.StoredProcedure;
                ecmd.Parameters.AddWithValue("@Email", viewModel.Email);
                SqlDataAdapter eadpt = new SqlDataAdapter(ecmd);
                DataSet eds = new DataSet();
                eadpt.Fill(eds);
                _context.Connect().Close();

                DataTable edt = eds.Tables[0];
                if (edt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {

                    List<CustomerPasswordChange> eCustomerList = eds.Tables[1].AsEnumerable().Select(row => new CustomerPasswordChange()
                    {


                        OldPassword = Convert.ToString(row["Password"]),

                    }).ToList();


                    AesReal aes = new AesReal();
                    string descryptpassword = aes.DecryptString(eCustomerList[0].OldPassword.ToString());
                    if (oldPassword == descryptpassword)
                    {
                        string enpass = aes.EnryptString(newPassword);
                        SqlCommand cmd = new SqlCommand("CustomerPasswordEdit", _context.Connect());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                        cmd.Parameters.AddWithValue("@CustomerId", viewModel.CustomerId);
                        cmd.Parameters.AddWithValue("@Password", enpass);
                        DateTime now = DateTime.Now;
                        cmd.Parameters.AddWithValue("@UpdatedDate", now);
                        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);
                        _context.Connect().Close();

                        dt = ds.Tables[0];
                        if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))

                        {

                            //    //    CustomerList = ds.Tables[1].AsEnumerable().Select(row => new CustomerPasswordChange()
                            //    //    {
                            //    //        CustomerId = Convert.ToString(row["CustomerId"]),
                            //    //        Email = Convert.ToString(row["Email"]),
                            //    //        UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                            //    //        NewPassword = Convert.ToString(row["Password"]),
                            //    //    }).ToList();

                        }
                        return new CustomerPasswordChangeModel()
                        {
                            // lstCustomerPasswordChange = CustomerList,
                            msg = new CommonMessageModel()
                            {
                                RespCode = dt.Rows[0][0].ToString(),
                                RespDesp = dt.Rows[0][1].ToString(),
                                RespMessageType = dt.Rows[0][2].ToString(),
                            }
                        };
                    }
                    else
                    {
                        return new CustomerPasswordChangeModel()
                        {
                            msg = new CommonMessageModel()
                            {
                                RespCode = "999",
                                RespDesp = edt.Rows[0][0].ToString(),
                                RespMessageType = Common.Message_ME//clolum 1
                            }
                        };
                    }

                }
                else
                {
                    return new CustomerPasswordChangeModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = "999",
                            RespDesp = edt.Rows[0][0].ToString(),//clolum 1
                            RespMessageType = Common.Message_ME
                        }
                    };
                }

            }
            catch (Exception e)
            {
                return new CustomerPasswordChangeModel()
                {
                    msg = new CommonMessageModel()
                    {
                        RespCode = "999",
                        RespDesp = e.Message,//clolum 1
                        RespMessageType = Common.Message_ME
                    }
                };
            }
        }
        #endregion PasswordEdit

        public TransactionByCustomerModel TransactionByCustomerId(string customerid)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("TransactionByCustomerId", _context.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", customerid);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _context.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    List<TransactionByCustomer> transactions = ds.Tables[1].AsEnumerable().
                       Select(row => new TransactionByCustomer()
                       {
                           SenderAccountNo = Convert.ToString(row[0]),
                           ReceiverAccountNo = Convert.ToString(row[1]),
                           TransactionAmount = Convert.ToDecimal(row[2]),
                           TransactionType = Convert.ToString(row[3]),
                           TransactionDate = Convert.ToDateTime(row[4]),

                       }).ToList();
                    return new TransactionByCustomerModel()
                    {
                        lstTransactionByCustomer = transactions,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString()
                        }
                    };
                }
                else
                {
                    return new TransactionByCustomerModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = "999",
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = Common.Message_ME
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new TransactionByCustomerModel()
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


        #region TopUpList
        public TopUpListModel TopUpList(string customerid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TopUpList", _context.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", customerid);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _context.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    List<TopUpList> topupList = ds.Tables[1].AsEnumerable().
                       Select(row => new TopUpList()
                       {
                           AccountType = Convert.ToString(row[0]),
                           AccountNo = Convert.ToString(row[1]),
                           TransactionAmount = Convert.ToDecimal(row[2]),
                           TransactionType = Convert.ToString(row[3]),
                           TransactionDate = Convert.ToDateTime(row[4]),

                       }).ToList();
                    return new TopUpListModel()
                    {
                        lstTopUpList = topupList,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString()
                        }
                    };
                }
                else
                {
                    return new TopUpListModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = "999",
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = Common.Message_ME
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new TopUpListModel()
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
        #endregion TopUpList


        #region transactionSummmary

        public TransactionSummeryModel TransactionSummery(string customerid)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("AllTransactionByCustId", _context.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", customerid);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _context.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    List<TransactionSummery> transactions = ds.Tables[1].AsEnumerable().
                       Select(row => new TransactionSummery()
                       {
                           CustomerName = Convert.ToString(row[0]),
                           Transaction_id = Convert.ToString(row[1]),
                           AccountNo = Convert.ToString(row[2]),
                           AccountType = Convert.ToString(row[3]),
                           TransactionAmount = Convert.ToDecimal(row["TransactionAmount"]),
                           Flash = Convert.ToString(row[5]),
                           TransactionType = Convert.ToString(row[6]),
                           TransactionDate = Convert.ToDateTime(row[7]),

                       }).ToList();
                    return new TransactionSummeryModel()
                    {
                        lstTransactionSummery = transactions,
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString()
                        }
                    };
                }
                else
                {
                    return new TransactionSummeryModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = "999",
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = Common.Message_ME
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new TransactionSummeryModel()
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

        #endregion transactionSummary

    }
}
