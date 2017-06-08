using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class AdminController : Controller
    {
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
                return RedirectToAction("Open_Admin_Page", "Admin","");

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        /// <summary>
        /// 開啟頁面
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Open_Admin_Page(string condition)
        {
            List<Model.BookData> GetData = new List<Model.BookData>();
            Service.SQL_AdminGetData SAGD = new Service.SQL_AdminGetData();
            GetData =  SAGD.GetRangeBookData();
            ViewBag.GetCertainData = GetData;
            return View();
        }

        public ActionResult CheckPurchaseBook(string[] GetPurchase,string[]ID)
        {

            for(int i = 0; i < ID.Count(); i++)
            {
                
            }
            return null;
        }
    }
}