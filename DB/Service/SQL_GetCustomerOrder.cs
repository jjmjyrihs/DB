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
    public class SQL_GetCustomerOrder
    {
        public List<Model.CustomerOrder> Get_Customer_Order(string Order_ID, string Customer_Email)
        {
            string sql = "";
            
            if (Order_ID == "")
            {
                sql = "select * from dbo.Customer_Order where Customer_Email='"+Customer_Email+"'";
            }
            else
            {
                sql = "select * from dbo.Customer_Order where Order_ID='" + Order_ID + "'";
            }            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            DataTable dt = new DataTable();
            
            using (conn)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                }
                catch (Exception e)
                {

                }
                conn.Close();
            }
            List<Model.CustomerOrder> Data = new List<Model.CustomerOrder>();
            Data = FillData(dt);
            return Data;
        }

        private List<Model.CustomerOrder> FillData(DataTable Getdata)
        {
            List<Model.CustomerOrder> result = new List<Model.CustomerOrder>();
            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.CustomerOrder
                    {
                        Order_ID = row["Order_ID"].ToString(),
                        Order_Date = Convert.ToDateTime(row["Order_Date"]).ToString("yyyy-MM-dd"),
                        Subscriber_Name = row["Subscriber_Name"].ToString(),
                        Subscriber_Cellphone = row["Subscriber_Cellphone"].ToString(),
                        Subscriber_Email = row["Subscriber_Email"].ToString(),
                        Subscriber_Address = row["Subscriber_Address"].ToString()

                    });
            }
            return result;
        }
    }
}
