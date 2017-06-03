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
        public List<Model.BookData> Find(string a)
        {
            if (a == null)
            {
                a = "";
            }
            string test = "\\'\" <>?!.;()=/";
            char[] splittemp = a.ToCharArray();
            string total = "";
            for (int i=0;i<splittemp.Length;i++)
            {
                    
                    
                    int c = test.IndexOf(splittemp[i]);
                    if (c < 0)
                    {
                        total += splittemp[i];
                    }
            }
            a = total;
            DataTable dt = new DataTable();
            string sql = @"select * from Books_Management where Book_Name like N'%[" + @a +"]%' or Book_Author like N'%[" + @a+ "]%'or Book_Press like N'%[" + @a + "]%'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
               // cmd.Parameters.Add(new SqlParameter(@a, SqlDbType.Int));

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
