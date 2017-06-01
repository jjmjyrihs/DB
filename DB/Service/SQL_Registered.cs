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
    public class SQL_Registered
    {
        public void Regis(Model.Customer a)
        {
            Random rd = new Random();
            DataTable dt = new DataTable();
            string name = a.Customer_Name;
            string cellphone = a.Customer_Cellphone;
            string Email = a.Customer_Email;
            string pwd = a.Customer_Password;
            string ID = "s" + DateTime.Now.ToString("yyyyMMdd") + cellphone.Substring(7,3) + rd.Next(100,999).ToString();
            string sql = "insert into Customer_Data(Customer_ID,Customer_Name,Customer_Email,Customer_Cellphone) values('"+ID+"','"+name+"','"+Email+"','"+cellphone+"')";
            string sql_pwd = "insert into Customer_Account(Customer_ID,Customer_Password) values('"+ID+"','"+pwd+"')";
            string set_Sql = sql + " " + sql_pwd;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(set_Sql, conn);
                // cmd.Parameters.Add(new SqlParameter(@a, SqlDbType.Int))
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            List<Model.BookData> Data = new List<Model.BookData>();
           
        }
    }
}
