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
    public class SQL_GetShoppingCart
    {
        public List<Model.ShippingCar> Find(string Customer_Email)
        {
            DataTable dt = new DataTable();
            string Checkout_Data_Customer_Email = Customer_Email;
            string sql = "select * from dbo.Shopping_Car where Customer_Email ='" + Checkout_Data_Customer_Email + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            List<Model.ShippingCar> Data = new List<Model.ShippingCar>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd_select = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter_check = new SqlDataAdapter(cmd_select);
                sqlAdapter_check.Fill(dt);

            }
            Data = FillData(dt);
            return Data;
        }

        private List<Model.ShippingCar>  FillData(DataTable dt)
        {
            List<Model.ShippingCar> result = new List<Model.ShippingCar>();
            foreach(DataRow row in dt.Rows)
            {
                result.Add(new Model.ShippingCar
                {
                    Customer_Email = row["Customer_Email"].ToString(),
                    Book_ID = row["Book_ID"].ToString(),
                    Book_Name = row["Book_Name"].ToString(),
                    Book_Price = row["Book_Price"].ToString(),
                    Book_Quantity = int.Parse(row["Book_Quantity"].ToString()),
                    Book_Img = row["Book_Img"].ToString(),
                    Order_Quantity = int.Parse(row["Order_Quantity"].ToString()),
                    
                });
            }
            return result;
        }
    }
}
