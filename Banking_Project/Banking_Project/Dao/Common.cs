using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Dao
{
    public class Common
    {
        public static string Message_ME = "ME"; // Error
        public static string Message_MS = "MS"; // Success
        public static string Message_MI = "MI"; // Information
        public static string Message_MW = "MW"; // Warning
        public static string ExceptionCode = "999";

        #region Check DB Column
        public static string IsDBNull(object value)
        {
            try
            {
                if (value == null) return null;
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return value.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int IsDBNullReturnZeroForInt(object value)
        {
            try
            {
                if (value == null) return 0;
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return Convert.ToInt32(value);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int? IsDBNullForInt(object value)
        {
            try
            {
                if (value == null) return null;
                if (!value.Equals(DBNull.Value))
                {
                    return Convert.ToInt32(value);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double IsDBNullReturnZeroForDouble(object value)
        {
            try
            {
                if (value == null) return 0;
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return Convert.ToDouble(value);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal IsDBNullReturnZeroForDecimal(object value)
        {
            try
            {
                if (value == null) return 0;
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return Convert.ToDecimal(value);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime? IsDBNullReturnNullForDateTime(object value)
        {
            try
            {
                if (value == null) return null;
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return Convert.ToDateTime(value);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime IsDBNullReturnDefaultForDateTime(object value)
        {
            try
            {
                if (value == null) return default(DateTime);
                if (!(value.Equals(System.DBNull.Value)))
                {
                    return Convert.ToDateTime(value);
                }
                return default(DateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
    public class CommonMessageModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }
        public string RespMessageType { get; set; }
    }
}