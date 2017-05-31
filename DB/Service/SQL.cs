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
    public class SQL
    {
        public List<Model.BookData> Find()
        {
            DataTable dt = new DataTable();
            string sql = "select * from Books_Management";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = FillData(dt);
            return Data;
        }

        private List<Model.BookData> FillData(DataTable Getdata)
        {
            List<Model.BookData> result = new List<Model.BookData>();
            foreach(DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.BookData {
                    Book_Name = row["Book_Name"].ToString(),
                    Book_Author = row["Book_Author"].ToString(),
                    Book_Press = row["Book_Press"].ToString(),
                    Book_Price = int.Parse(row["Book_Price"].ToString()),
                    Book_Img = row["Book_Img"].ToString()
                    });
            }
            return result;
        }
    }
}
