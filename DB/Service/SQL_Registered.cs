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
        public string Regis(Model.CustomerData a)
        {
            DataTable dt = new DataTable();
            bool Data;
            string name = a.Customer_Name;
            string cellphone = a.Customer_Cellphone;
            string Email = a.Customer_Email;
            string pwd = a.Customer_Password;
            string sql_check = "select * from Customer_Data where Customer_Email = '" + Email + "';";
            string sql = "insert into Customer_Data(Customer_Email,Customer_Name,Customer_Cellphone) values('"+ Email + "','"+name+"','"+cellphone+"')";
            string sql_pwd = "insert into Customer_Account(Customer_Email,Customer_Password) values('"+Email+"','"+pwd+"')";
            string set_Sql = sql + " " + sql_pwd;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd_check = new SqlCommand(sql_check, conn);
                SqlDataAdapter sqlAdapter_check = new SqlDataAdapter(cmd_check);
                sqlAdapter_check.Fill(dt);
                Data = FillData(dt);
                if (Data == true)
                {
                    
                    SqlCommand cmd = new SqlCommand(set_Sql, conn);
                    // cmd.Parameters.Add(new SqlParameter(@a, SqlDbType.Int))
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Email;
                }
                else
                {
                    //帳號已存在視窗.
                    conn.Close();
                    return null;
                }
                
            }
        }

        private bool FillData(DataTable Getdata)
        {
            List<Model.CustomerData> result = new List<Model.CustomerData>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.CustomerData
                    {
                        Customer_Email = row["Customer_Email"].ToString(),
                    });
            }
            if (result.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
