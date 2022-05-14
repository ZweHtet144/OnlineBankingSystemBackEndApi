using Banking.Dao;
using Banking_Project.Dao;
using Banking_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking_Project.Services
{
    public class CustomerService
    {
        private readonly SqlDataAccess _conn = new SqlDataAccess();
        public List<Customer> SelectCustomers()
        {
            SqlCommand cmd = new SqlCommand("select * from Customer where IsDeleted='False';", _conn.Connect());
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            _conn.Connect().Close();

            List<Customer> lst = ds.Tables[0].AsEnumerable().
                   Select(row => new Customer()
                   {
                       CustomerId = Convert.ToString(row["CustomerId"]),
                       CustomerName = Convert.ToString(row["CustomerName"]),
                       NRC = Convert.ToString(row["NRC"]),
                       Phoneno = Convert.ToString(row["Phoneno"]),
                       Email = Convert.ToString(row["Email"]),
                       Address = Convert.ToString(row["Address"]),
                       CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                   }).ToList();
            return lst;
        }
        public void CreateCustomer(Customer reqModel)
        {
            string Password = reqModel.Password;
            AesReal aes = new AesReal();
            string enpassword = aes.EnryptString(Password);
            try
            {
                SqlCommand cmd = new SqlCommand("CreateCustomer", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Name", reqModel.CustomerName);
                cmd.Parameters.AddWithValue("Email", reqModel.Email);
                cmd.Parameters.AddWithValue("Password", enpassword);
                cmd.Parameters.AddWithValue("NRC", reqModel.NRC);
                cmd.Parameters.AddWithValue("Phoneno", reqModel.Phoneno);
                cmd.Parameters.AddWithValue("Address", reqModel.Address);
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
        public Customer CustomerWithId(string customerid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Customer where CustomerId=@customerid;", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("CustomerId", customerid);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                //var cust1 = ds.Tables[0].AsEnumerable().ToList();
                Customer cust = ds.Tables[0].AsEnumerable().
                       Select(row => new Customer()
                       {
                           CustomerId = Convert.ToString(row["CustomerId"]),
                           CustomerName = Convert.ToString(row["CustomerName"]),
                           Email = Convert.ToString(row["Email"]),
                           Password = Convert.ToString(row["Password"]),
                           NRC = Convert.ToString(row["NRC"]),
                           Phoneno = Convert.ToString(row["Phoneno"]),
                           Address = Convert.ToString(row["Address"]),

                       }).FirstOrDefault();
                return cust;
            }
            catch (Exception e)
            {
                return new Customer();

            }


        }
        public void Update(Customer reqModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateCustomer", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerName", reqModel.CustomerName);
                cmd.Parameters.AddWithValue("Email", reqModel.Email);
                cmd.Parameters.AddWithValue("NRC", reqModel.NRC);
                cmd.Parameters.AddWithValue("Phoneno", reqModel.Phoneno);
                cmd.Parameters.AddWithValue("Address", reqModel.Address);
                cmd.Parameters.AddWithValue("CustomerId", reqModel.CustomerId);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
            }
            catch (Exception e)
            {

            }

        }

        //public void CreateBankAccount(Account reqModel)
        //{
        //    SqlCommand cmd = new SqlCommand("CreateBankAccount", _conn.Connect());
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("AccountType", reqModel.AccountType);
        //    cmd.Parameters.AddWithValue("Amount", reqModel.Amount);
        //    cmd.Parameters.AddWithValue("CustomerId", reqModel.CustomerId);
        //    cmd.Parameters.AddWithValue("IsDeleted", "False");
        //    cmd.Parameters.AddWithValue("Version", 1);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    _conn.Connect().Close();
        //}
        //public CustomerModel Delete(string customerid)
        //{
        //    try
        //    {
        //        if (customerid != null)
        //        {
        //            SqlCommand cmd = new SqlCommand("Update customer set IsDeleted='True' where customerid=@customerid", _conn.Connect());
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("CustomerId", @customerid);
        //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            adp.Fill(ds);
        //            _conn.Connect().Close();
        //            return new CustomerModel()
        //            {
        //                msg = new CommonMessageModel()
        //                {
        //                    RespCode = "999",
        //                    RespDesp = "Success",
        //                    RespMessageType = "MS"
        //                }
        //            };
        //        }
        //        else
        //        {
        //            return new CustomerModel()
        //            {
        //                msg = new CommonMessageModel()
        //                {
        //                    RespCode = "000",
        //                    RespDesp = "Failed",
        //                    RespMessageType = "ME"
        //                }
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return new CustomerModel()
        //        {
        //            msg = new CommonMessageModel()
        //            {
        //                RespCode = "000",
        //                RespDesp = "Failed",
        //                RespMessageType = "ME"
        //            }
        //        };
        //    }


        //}

        public CustomerModel Delete(string CustomerId)
        {
            CustomerModel model = new CustomerModel();
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteCustomer", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", @CustomerId);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    model.msg = new CommonMessageModel()
                    {
                        RespCode = dt.Rows[0][0].ToString(),
                        RespDesp = dt.Rows[0][1].ToString(),
                        RespMessageType = dt.Rows[0][2].ToString(),
                    };
                    //return model;

                }
                //model.msg = new CommonMessageModel()
                //{
                //    RespCode = dt.Rows[0][0].ToString(),
                //    RespDesp = dt.Rows[0][1].ToString(),
                //    RespMessageType = dt.Rows[0][2].ToString(),
                //};
                return model;
            }
            catch (Exception ex)
            {
                model.msg = new CommonMessageModel()
                {
                    RespCode = Common.ExceptionCode,
                    RespDesp = ex.Message,
                    RespMessageType = Common.Message_ME
                };
                return model;
            }


        }
        public List<CustomerAccounts> CustomerAccounts()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[CustomerAccountInfo]", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();

                List<CustomerAccounts> lst = ds.Tables[0].AsEnumerable().
                      Select(row => new CustomerAccounts()
                      {
                          CustomerId = Convert.ToString(row["CustomerId"]),
                          CustomerName = Convert.ToString(row["CustomerName"]),
                          NRC = Convert.ToString(row["NRC"]),
                          Phoneno = Convert.ToString(row["Phoneno"]),
                          AccountNo = Convert.ToString(row["AccountNo"]),
                          Amount = Convert.ToInt32(row["Amount"]),
                          AccountType = Convert.ToString(row["AccountType"]),
                          CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                      }).ToList();
                return lst;
            }
            catch (Exception e)
            {
                var lis = new List<CustomerAccounts>();
                return lis;
            }

        }

        public void AccountDelete(string AccountNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update AccountInfo set IsDeleted='True' where AccountNo=@accountno", _conn.Connect());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("AccountNo", @AccountNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
            }
            catch (Exception e)
            {

            }

        }
        #region searchbyCustomerId
        public TransactionByCustomerModel TransactionByCustomerId(string CustomerId)
        {
            try
            {
                var model = new TransactionByCustomerModel();
                SqlCommand cmd = new SqlCommand("TransactionByCustomerId", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", CustomerId);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    model.lstTransactionByCustomer = ds.Tables[1].AsEnumerable().
                    Select(row => new TransactionByCustomer()
                    {
                        SenderAccountNo = Convert.ToString(row[0]),
                        ReceiverAccountNo = Convert.ToString(row[1]),
                        TransactionAmount = Convert.ToDecimal(row[2]),
                        TransactionDate = Convert.ToDateTime(row[4]),
                    }).ToList();
                    model.msg = new CommonMessageModel()
                    {
                        RespCode = dt.Rows[0][0].ToString(),
                        RespDesp = dt.Rows[0][1].ToString(),
                        RespMessageType = dt.Rows[0][2].ToString()
                    };
                    return model;
                }
                else
                {
                    return new TransactionByCustomerModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString(),
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
        #endregion searchbyCustomerId


        #region searchByDayRange
        public TransactionByDayRangeModel TransactionByStartDateEndDate(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var model = new TransactionByDayRangeModel();
                SqlCommand cmd = new SqlCommand("TransactionByDayRange", _conn.Connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("StartDate", StartDate);
                cmd.Parameters.AddWithValue("EndDate", EndDate);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                _conn.Connect().Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0][2].ToString().Equals(Common.Message_MS))
                {
                    model.lstDayRange = ds.Tables[1].AsEnumerable().
                    Select(row => new TransactionByDayRange()
                    {
                        SenderAccountNo = Convert.ToString(row[0]),
                        ReceiverAccountNo = Convert.ToString(row[1]),
                        TransactionAmount = Convert.ToDecimal(row[2]),
                        TransactionDate = Convert.ToDateTime(row[3]),
                    }).ToList();
                    model.msg = new CommonMessageModel()
                    {
                        RespCode = dt.Rows[0][0].ToString(),
                        RespDesp = dt.Rows[0][1].ToString(),
                        RespMessageType = dt.Rows[0][2].ToString()
                    };
                    return model;
                }
                else
                {
                    return new TransactionByDayRangeModel()
                    {
                        msg = new CommonMessageModel()
                        {
                            RespCode = dt.Rows[0][0].ToString(),
                            RespDesp = dt.Rows[0][1].ToString(),
                            RespMessageType = dt.Rows[0][2].ToString(),
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new TransactionByDayRangeModel()
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
        #endregion searchByDayRange

    }
}