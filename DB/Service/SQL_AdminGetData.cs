﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Service
{
    public class SQL_AdminGetData
    {
        /// <summary>
        /// 獲取庫存大於10或小於3之書本資料
        /// </summary>
        /// <returns></returns>
        public List<Model.BookData> GetRangeBookData()
        {
            string sql = "select * from Books_Management where Book_Quantity>10 or Book_Quantity<3 order by Book_Quantity";
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
            Data = FillData(dt);
            return Data;
        }

        private List<Model.BookData> FillData(DataTable Getdata)
        {
            List<Model.BookData> result = new List<Model.BookData>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.BookData
                    {
                        Book_ID = row["Book_ID"].ToString(),
                        Book_Name = row["Book_Name"].ToString(),
                        Book_Author = row["Book_Author"].ToString(),
                        Book_Press = row["Book_Press"].ToString(),
                        Book_Price = row["Book_Price"].ToString(),
                        Book_Quantity = int.Parse(row["Book_Quantity"].ToString())
                    });
            }
            return result;
        }
    }
    
}
