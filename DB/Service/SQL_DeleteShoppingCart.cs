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
    public class SQL_DeleteShoppingCart
    {
        public void Delete_Cart()
        {
            string sql = "delete from dbo.Shopping_Car";
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
