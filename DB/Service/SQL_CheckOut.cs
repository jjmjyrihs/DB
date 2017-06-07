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
    public class SQL_CheckOut
    {
        public void InsertCustomer_Order(Model.CustomerOrder Data)
        {
            //Customer_Order sql
            string sql_ID = "insert into dbo.Customer_Order(Order_ID) values('" + Data.Order_ID + "'); ";
            string sql = "update dbo.Customer_Order set " +
                "Customer_Email = '" + Data.Customer_Email + "'," +
                "Order_Date = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'," +
                "Subscriber_Name = '" + Data.Subscriber_Name + "'," +
                "Subscriber_Cellphone = '" + Data.Subscriber_Cellphone + "'," +
                "Subscriber_Email = '" + Data.Subscriber_Email + "'," +
                "Subscriber_Address = '" + Data.Subscriber_Address + "' where Order_ID = '" + Data.Order_ID + "';";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                /*  SqlCommand cmd = new SqlCommand(sql_setID, conn);
                  SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                  cmd.ExecuteNonQuery();*/
                SqlCommand cmd_sql_ID = new SqlCommand(sql_ID, conn);
                SqlCommand cmd_sql = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter_sql_ID = new SqlDataAdapter(cmd_sql_ID);
                SqlDataAdapter sqlAdapter_sql = new SqlDataAdapter(cmd_sql);
                try
                {
                    cmd_sql_ID.ExecuteNonQuery();
                    cmd_sql.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
                conn.Close();
            }
            //sql_Book_ID

        }

        public void InsertOrder_Books(Model.CustomerOrder Data, List<Model.ShippingCar> Data2)
        {
            for (int i = 0; i < Data2.Count; i++)
            {
                string sql = "insert into dbo.Order_Books(Order_ID,Book_ID,Order_Quantity) " +
                    "values('" + Data.Order_ID + "','" + Data2[i].Book_ID + "','" + Data2[i].Order_Quantity + "')";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
                using (conn)
                {
                    conn.Open();
                    SqlCommand cmd_sql = new SqlCommand(sql, conn);
                    SqlDataAdapter sqlAdapter_sql = new SqlDataAdapter(cmd_sql);
                    try
                    {
                        cmd_sql.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    conn.Close();
                }
            }
        }
    }
}
