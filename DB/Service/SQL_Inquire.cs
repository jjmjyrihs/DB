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
    public class SQL_Inquire
    {
        public List<Model.BookData> Find(string Book_Search, bool All)
        {
            string sql = "";
            if (All == true)
            {
                sql = "select * from Books_Management";
            }
            else
            {
                string test = "\\'\" <>?!.;()=/";
                char[] splittemp = Book_Search.ToCharArray();
                string total = "";
                for (int i = 0; i < splittemp.Length; i++)
                {
                    int c = test.IndexOf(splittemp[i]);
                    if (c < 0)
                    {
                        total += splittemp[i];
                    }
                }
                Book_Search = total;
                sql = @"select * from Books_Management where Book_Name like N'%[" + @Book_Search + "]%' or Book_Author like N'%[" + @Book_Search + "]%'or Book_Press like N'%[" + @Book_Search + "]%'";
            }            
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
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = FillData(dt, Book_Search);
            return Data;
        }

        private List<Model.BookData> FillData(DataTable Getdata,string Book_Search)
        {
            List<Model.BookData> result = new List<Model.BookData>();
            
            foreach(DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.BookData {
                        Book_ID = row["Book_ID"].ToString(),
                        Book_Name = row["Book_Name"].ToString(),
                        Book_Author = row["Book_Author"].ToString(),
                        Book_Press = row["Book_Press"].ToString(),
                        Book_Price = row["Book_Price"].ToString(),
                        Book_Img = row["Book_Img"].ToString(),
                        Book_Quantity = int.Parse(row["Book_Quantity"].ToString()),
                        Book_Search = Book_Search
                    });
            }
            return result;
        }
    }
}
