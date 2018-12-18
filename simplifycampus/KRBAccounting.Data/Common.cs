using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace KRBAccounting.Data
{
    public static class Common
    {
        public static Int32 ToInt32(this object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString() == String.Empty)
            {
                return 0;
            }
            try
            {
                //return Convert.ToInt32(obj, NumberStyles.Any);
                int result = 0;
                int.TryParse(obj.ToStr(), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out result);
                return result;
            }
            catch { return 0; }
        }

        public static decimal ToDecimal(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            try
            {
                //return Convert.ToDecimal(obj);
                decimal result = 0m;
                decimal.TryParse(obj.ToStr(), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out result);
                return result;
            }
            catch { return 0; }
        }
        public static double ToDouble(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            try
            {
                double result = 0;
                double.TryParse(obj.ToStr(), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out result);
                return result;
            }
            catch { return 0; }
        }

        public static Boolean ToBoolean(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch { return false; }
        }

        public static string ToStr(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            try
            {
                return Convert.ToString(obj);
            }
            catch { return string.Empty; }
        }
        public static DateTime ToDate(this object ob)
        {
            if (ob == null || ob == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(ob);
            }
            catch
            {
                return DateTime.MinValue;
            }

        }

        public static bool IsDecimal(this object obj)
        {
            string str = obj.ToStr();
            string[] arr = str.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length > 1)
            {
                if (arr[1].ToInt32() > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static void CloseConnection(this SqlConnection conn)
        {
            if (conn != null)
            {
                if (conn.State != ConnectionState.Closed)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch { }
                }
            }
        }

        public static string Encrypt(this string clearText)
        {
            string ret = string.Empty;
            if (!string.IsNullOrEmpty(clearText))
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(clearText);
                data = md5Hasher.ComputeHash(data);
                for (int i = 0; i < data.Length; i++)
                {
                    ret += data[i].ToString("x2").ToLower();
                }
            }
            return ret;
        }

    }
}
