using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class AdminController : Controller
    {
        Service.SQL_AdminGetData SAGD = new Service.SQL_AdminGetData();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 使用者帳戶確認
        /// </summary>
        /// <param name="Admin_Account"></param>
        /// <param name="Admin_Password"></param>
        /// <returns></returns>
        public ActionResult Admin_Check(string Admin_Account, string Admin_Password)
        {
            if (Admin_Account == "PleaseHackMe" && Admin_Password == "123")
            {
                return RedirectToAction("PurchaseList", "Admin","");

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        /// <summary>
        /// 取得最一開始主頁數量小於3書籍function
        /// </summary>
        public void GetHomeData()
        {
            List<Model.BookData> GetData = new List<Model.BookData>();
            List<Model.Admin> GetAllData = new List<Model.Admin>();
            List<Model.Admin> SendProcessingData = new List<Model.Admin>();
            List<Model.Admin> SendProcessedData = new List<Model.Admin>();

            GetData = SAGD.GetRangeBookData();
            GetAllData = SAGD.PurchaseList();

            for (int i = 0; i < GetAllData.Count; i++)
            {
                if (GetAllData[i].Processing_Static == "False")
                {
                    SendProcessingData.Add(GetAllData[i]);
                }
                if (new TimeSpan(DateTime.Now.Ticks
                    - Convert.ToDateTime(GetAllData[i].Order_Date).Ticks).TotalDays <= 3
                    && GetAllData[i].Processing_Static == "True")
                {
                    SendProcessedData.Add(GetAllData[i]);
                }
            }
            ViewBag.GetCertainData = GetData;
            ViewBag.Count = GetData.Count;
            ViewBag.GetAllData = GetAllData;
            ViewBag.GetProcessingData = SendProcessingData;
            ViewBag.GetProcessedData = SendProcessedData;
        }

        /// <summary>
        /// 開啟主頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseList()
        {
            GetHomeData();
            return View();
        }
        /// <summary>
        /// 進貨
        /// </summary>
        /// <param name="GetPurchase"></param>
        /// <param name="Book_ID"></param>
        /// <returns></returns>
        public ActionResult CheckPurchaseBook(string[] GetPurchase,string[]Book_ID)
        {
            string OrderID = SAGD.NewPurchaseOrderID();
            for(int i = 0; i < Book_ID.Count(); i++)
            {
                if (GetPurchase[i].ToString() != "")
                {
                    SAGD.Purchase_Book_Data(GetPurchase[i], Book_ID[i],OrderID);
                }
            }
            return RedirectToAction("RedirectToAdminHome", "Admin");
        }

        /// <summary>
        /// 將未處理訂單之書籍更新至書庫裡面
        /// </summary>
        public ActionResult UpdateStock(string[] Update)
        {
            string[] Book_Data;
            for(int i = 0; i < Update.Length; i++)
            {
                Book_Data = Update[i].Split(' ');
                SAGD.UpdateStock(Book_Data[0].ToString(), Book_Data[1],Book_Data[2]);
            }
            return RedirectToAction("PurchaseList","Admin");
        }
        /// <summary>
        /// 所有銷貨資料
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleList()
        {
            GetHomeData();
           List<List<Model.Admin>> GetSaleList = new List<List<Model.Admin>>();
            List<List<Model.Admin>> GetAllCompleteSaleList = new List<List<Model.Admin>>();
            GetSaleList =  SAGD.GetSaleList((bool)false);
            GetAllCompleteSaleList = SAGD.GetSaleList((bool)true);
            if (GetSaleList == null)
            {
                return RedirectToAction("SaleList", "Admin");
            }
            ViewBag.GetSaleList = GetSaleList;
            ViewBag.GetAllCompleteSaleList = GetAllCompleteSaleList;
            return View();
        }

        public ActionResult Shipments(string[] Shipment)
        {
            
            for(int i = 0; i < Shipment.Length; i++)
            {
                SAGD.ShipmentAndUpdate(Shipment[i]);
            }
            return RedirectToAction("SaleList","Admin");
        }

        

        public ActionResult Book_Management()
        {
            GetHomeData();
            Service.SQL_Inquire SI = new Service.SQL_Inquire();
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = SI.Find("", true);
            ViewBag.AllBooks = Data;
            return View();
        }
        /// <summary>
        /// sql書籍新增
        /// </summary>
        /// <param name="Book_ID"></param>
        /// <param name="Book_Name"></param>
        /// <param name="Book_Author"></param>
        /// <param name="Book_Press"></param>
        /// <param name="Book_Price"></param>
        /// <param name="Book_Quantity"></param>
        /// <param name="Book_Img"></param>
        /// <returns></returns>
        public ActionResult AddBook(string Book_ID,string Book_Name,string Book_Author,string Book_Press,string Book_Price,string Book_Quantity,string Book_Img)
        {
            SAGD.InsertBook(Book_ID, Book_Name, Book_Author, Book_Press, Book_Price, Book_Quantity, Book_Img);
            return RedirectToAction("Add_Books","Admin");
        }
        /// <summary>
        /// 開啟新增書籍頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add_Books()
        {
            GetHomeData();
            return View();
        }
        /// <summary>
        /// 商品下架
        /// </summary>
        /// <returns></returns>
        public ActionResult Down(string[] ISBN)
        {
            for(int i =0;i<ISBN.Length;i++)
            {
                SAGD.Down(ISBN[i]);
            }
            return RedirectToAction("Book_Management", "Admin");
        }





        public ActionResult RedirectToAdminHome()
        {
            return View();
        }

      
    }
}