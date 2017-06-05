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
    /// <summary>
    /// 新增書籍至購物車
    /// </summary>
    public class SQL_ShippingCar
    {
        public void ShoppingCart(Model.ShippingCar Data)
        {
            string sql = "";
            if (Data.Action == "Insert")
            {
                sql = "insert into dbo.Shopping_Car(Book_ID, Customer_Email, Book_Name, Book_Price, Book_Quantity,Book_Img,Order_Quantity) values ('" + Data.Book_ID + "','" + Data.Customer_Email + "','" + Data.Book_Name + "','" + Data.Book_Price + "','" + Data.Book_Quantity + "','" + Data.Book_Img + "','1')";
            }
            else
            {
                sql = "delete from dbo.Shopping_Car where Customer_Email = '"+Data.Customer_Email+"' and Book_ID = '"+Data.Book_ID+"'";
            }            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd_insert = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter_check = new SqlDataAdapter(cmd_insert);
                try
                {
                    cmd_insert.ExecuteNonQuery();
                }catch(Exception e)
                {
                    
                }
                conn.Close();
            }
        }
    }
}
