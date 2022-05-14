using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking.Dao
{
    public class SqlDataAccess
    {
        private SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        private SqlConnection conn = new SqlConnection();

        //Connect To DB
        public SqlConnection Connect()
        {
            if (conn == null) conn = new SqlConnection(sb.ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
            {
                return conn;
            }
            conn = new SqlConnection(sb.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}