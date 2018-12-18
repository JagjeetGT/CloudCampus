using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KRBAccounting.Data
{
    public class ConnectionString
    {
        public static SqlConnection GetConnectionString()
        {
            string str = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(str);
            return conn;
        }

        public static DataTable GetDataTable(string ProcName, SqlParameter[] param)
        {
            DataTable dt = null;
            using (SqlConnection con = GetConnectionString())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        dt.Clear();
                        da.Fill(dt);
                    }
                }
                return dt;
            }
        }
    }
}
