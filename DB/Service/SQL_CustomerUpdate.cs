using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Service
{
    public class SQL_CustomerUpdate
    {
        public Boolean Update(Model.CustomerData Data)
        {
            string sql = "Update dbo.Customer_Data set ";
            
            if (Data.Customer_Name != null)
            {
                sql += "Customer_Name = '" + Data.Customer_Name + "' , ";
            }
            if (Data.Customer_Cellphone != null)
            {
                sql += " Customer_Cellphone = '" + Data.Customer_Cellphone + "'";
            }
            if (Data.Customer_Password != null)
            {
                sql += "Update dbo.Customer_Account set ";
                sql += " Customer_Password = '" + Data.Customer_Password;
            }
            sql += "' where Customer_Email = '" + Data.Customer_Email + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }
    }
}
