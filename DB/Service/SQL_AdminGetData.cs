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
    public class SQL_AdminGetData
    {
        /// <summary>
        /// 獲取庫存小於5之書本資料
        /// </summary>
        /// <returns></returns>
        public List<Model.BookData> GetRangeBookData()
        {
            string sql = "select * from Books_Management where Book_Quantity<=5 order by Book_Quantity";
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

        /// <summary>
        /// 獲取進貨訂單
        /// </summary>
        /// <returns></returns>
        public List<Model.Admin> PurchaseList()
        {
            string sql = "select * from dbo.Books_Management bm join dbo.Purchase_Order_Data pod on bm.Book_ID = pod.Book_ID join dbo.Purchase_Order po on pod.Order_ID = po.Order_ID";
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
            List<Model.Admin> Data = new List<Model.Admin>();
            Data = Fill_Data(dt);
            return Data;
        }

        private List<Model.Admin> Fill_Data(DataTable Getdata)
        {
            List<Model.Admin> result = new List<Model.Admin>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.Admin
                    {
                        Book_ID = row["Book_ID"].ToString(),
                        Book_Name = row["Book_Name"].ToString(),
                        Book_Author = row["Book_Author"].ToString(),
                        Book_Press = row["Book_Press"].ToString(),
                        Book_Price = row["Book_Price"].ToString(),
                        Purchase_Quantity = row["Purchase_Quantity"].ToString(),
                        Order_ID = row["Order_ID"].ToString(),
                        Processing_Static = row["Processing_Static"].ToString(),
                        Order_Date = row["Order_Date"].ToString()
                        
                    });
            }
            return result;
        }
        /// <summary>
        /// 新增進貨訂單編號
        /// </summary>
        public string  NewPurchaseOrderID()
        {
            List<Model.Admin> Temp = new List<Model.Admin>();
            Temp = PurchaseList();
            Random rd = new Random();
            string Order_ID = DateTime.Now.ToString("yyyyMMdd") + rd.Next(0, 10) + rd.Next(0, 10) + rd.Next(0, 10)
                + rd.Next(0, 10) + rd.Next(0, 10) + Temp.Count;
            string sql = "insert into Purchase_Order(Order_ID,Order_Date) values('" + Order_ID + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "');";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd_sql_ID = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter_sql = new SqlDataAdapter(cmd_sql_ID);
                cmd_sql_ID.ExecuteNonQuery();
            }
            return Order_ID;
        }
        /// <summary>
        /// 新增訂單書籍資料
        /// </summary>
        /// <param name="GetPurchaseQuantity"></param>
        /// <param name="Book_ID"></param>
        public void Purchase_Book_Data(string GetPurchaseQuantity , string Book_ID,string Order_ID)
        {
            string sql = "insert into dbo.Purchase_Order_Data(Order_ID,Book_ID,Purchase_Quantity,Processing_Static)" +
                "values('" + Order_ID + "','" + Book_ID + "','" + GetPurchaseQuantity + "','False');";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter_sql = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// 處理未處理進貨訂單
        /// </summary>
    
        public void UpdateStock(string Order_ID,string Book_ID,string Quantity)
        {
            string sql_Update_Stock = "update Books_Management set Book_Quantity = Book_Quantity + " + Quantity + "where Book_ID = " +
                "'" + Book_ID + "';";
            string sql_Update_Processing_Static = "update Purchase_Order_Data set Processing_Static = 'True' " +
                "where Order_ID = '" + Order_ID + "' and Book_ID = '" + Book_ID + "';";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd_Update_Stock = new SqlCommand(sql_Update_Stock, conn);
                SqlDataAdapter sqlAdapter_sql = new SqlDataAdapter(cmd_Update_Stock);
                SqlCommand cmd_Update_Processing_Static = new SqlCommand(sql_Update_Processing_Static, conn);
                SqlDataAdapter sqlAdapter_sql_2 = new SqlDataAdapter(cmd_Update_Processing_Static);
                cmd_Update_Stock.ExecuteNonQuery();
                cmd_Update_Processing_Static.ExecuteNonQuery();
                conn.Close();
            }

        }
        /// <summary>
        /// 取得所有銷貨訂單ID，做二維list
        /// </summary>
        /// <returns></returns>
        public List<List<Model.Admin>> GetSaleList()
        {
            //取得所有訂單編號
            string sql_GetID = "select * from Customer_Order";
            
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_GetID, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            List<Model.Admin> Data = new List<Model.Admin>();
            List<List<Model.Admin>> IDList = new List<List<Model.Admin>>();
            Data = Fill_ID_SaleData(dt);
            IDList = GetSaleDetail(Data);
            return IDList;
        }

        public List<List<Model.Admin>> GetSaleDetail(List<Model.Admin> GetIDList)
        {
            //取得明細
            List < List<Model.Admin> > IDList= new List<List<Model.Admin>>();
            //List<Model.Admin> DetailList = new List<Model.Admin>();
            for(int i = 0; i < GetIDList.Count; i++)
            {
                string sql_GetDetail = "select * from Books_Management bm join Order_Books ob on bm.Book_ID = ob.Book_ID where ob.Order_ID = '" +
                GetIDList[i].Order_ID + "';";
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString);
                using (conn)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetDetail, conn);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    conn.Close();
                }
                List<Model.Admin> Data = new List<Model.Admin>();
                Data = Fill_DetailSaleData(dt);
                Data.Add(GetIDList[i]);
                IDList.Add(Data);
            }
            
            return IDList;
        }

        private List<Model.Admin> Fill_ID_SaleData(DataTable Getdata)
        {
            List<Model.Admin> result = new List<Model.Admin>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.Admin
                    {
                        Order_ID = row["Order_ID"].ToString(),
                        Customer_Email = row["Customer_Email"].ToString(),
                       // Book_ID = row["Book_ID"].ToString(),
                      //  Book_Name = row["Book_Name"].ToString(),
                       // Book_Author = row["Book_Author"].ToString(),
                      //  Book_Press = row["Book_Press"].ToString(),
                      //  Book_Price = row["Book_Price"].ToString(),
                     //   Order_Quantity = row["Order_Quantity"].ToString(),
                        Subscriber_Name = row["Subscriber_Name"].ToString(),
                        Subscriber_Cellphone = row["Subscriber_Cellphone"].ToString(),
                        Subscriber_Email = row["Subscriber_Email"].ToString(),
                        Subscriber_Address = row["Subscriber_Address"].ToString(),
                        Order_Date = row["Order_Date"].ToString(),
                        Shipping_Date = row["Shipping_Date"].ToString(),
                        Sale_Processing_Static = row["Sale_Processing_Static"].ToString()
                    });
            }
            return result;
        }
        private List<Model.Admin> Fill_DetailSaleData(DataTable Getdata)
        {
            List<Model.Admin> result = new List<Model.Admin>();

            foreach (DataRow row in Getdata.Rows)
            {
                result.Add(
                    new Model.Admin
                    {
                         Book_ID = row["Book_ID"].ToString(),
                          Book_Name = row["Book_Name"].ToString(),
                         Book_Author = row["Book_Author"].ToString(),
                          Book_Press = row["Book_Press"].ToString(),
                          Book_Price = row["Book_Price"].ToString(),
                           Order_Quantity = row["Order_Quantity"].ToString()
                    });
            }
            return result;
        }
    }



}
