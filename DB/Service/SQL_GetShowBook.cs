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
    public class SQL_GetShowBook
    {
        public List<Model.ShowBook> ShowBook(string Order_ID)
        {
            string sql = "select * from dbo.Order_Books a join dbo.Books_ManageMent b " +
                "on a.Book_ID = b.Book_ID where Order_ID = '" + Order_ID + "'";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<Model.ShowBook> Data = new List<Model.ShowBook>();
            Data = FillData(dt);
            return Data;
        }

        private List<Model.ShowBook> FillData(DataTable Getdata)
        {
            List<Model.ShowBook> result = new List<Model.ShowBook>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.ShowBook
                    {
                        Order_ID = row["Order_ID"].ToString(),
                        Book_Name = row["Book_Name"].ToString(),
                        Order_Quantity = row["Order_Quantity"].ToString(),
                        Book_Price = row["Book_Price"].ToString()
                    });
            }
            return result;
        }
    }
    }

