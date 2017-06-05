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
    public class SQL_CustomerCheck
    {
        public List<Model.CustomerData> Check(Model.CustomerData Cusdata)
        {
            string account = Cusdata.Customer_Email;
            string pwd = Cusdata.Customer_Password;
            string sql = @"select ca.Customer_Password,cd.Customer_Email from Customer_Data cd join Customer_Account ca on cd.Customer_Email=ca.Customer_Email where cd.Customer_Email = @Customer_Email";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                // cmd.Parameters.Add(new SqlParameter(@a, SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Customer_Email", account));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<Model.CustomerData> Data = new List<Model.CustomerData>();
                Data = FillData(dt);
            return Data;
        }

        public List<Model.CustomerData> FillData(DataTable a)
        {
            List<Model.CustomerData> result = new List<Model.CustomerData>();
            foreach(DataRow dr in a.Rows)
            {
                result.Add(new Model.CustomerData {
                    Customer_Email = dr["Customer_Email"].ToString(),
                    Customer_Password = dr["Customer_Password"].ToString()
                });
            }
            return result;
        }
    }
}
