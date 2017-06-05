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
    public class SQL_CustomerData
    {
        public Model.CustomerData getData(string Account)
        {
            Model.CustomerData Data = new Model.CustomerData();
            DataTable dt = new DataTable();
            string sql = "select * from dbo.Customer_Data where Customer_Email = '" + Account + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            Data = FillData(dt);
            return Data;
        }
        private Model.CustomerData FillData(DataTable dt)
        {
            Model.CustomerData result = new Model.CustomerData();
            foreach (DataRow row in dt.Rows)
            {
                result.Customer_Name = row["Customer_Name"].ToString();
                result.Customer_Cellphone = row["Customer_Cellphone"].ToString();
                result.Customer_Email = row["Customer_Email"].ToString();
            }
            return result;
        }
    }
}
