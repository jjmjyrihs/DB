using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class Order_SearchController : Controller
    {
        // GET: Order_Search
        public ActionResult Index()
        {
            string Customer_Email = "";
            try
            {
                Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
                Service.SQL_GetCustomerOrder SGCO = new Service.SQL_GetCustomerOrder();
                Service.SQL_GetShowBook SGSB = new Service.SQL_GetShowBook();

                List<Model.CustomerOrder> Data = new List<Model.CustomerOrder>();
                List<Model.ShowBook> Data2 = new List<Model.ShowBook>();
                List<List<Model.ShowBook>> test = new List<List<Model.ShowBook>>();
                Data = SGCO.Get_Customer_Order("",Customer_Email);
                for (int i = 0; i < Data.Count; i++)
                {
                    Data2 = SGSB.ShowBook(Data[i].Order_ID.ToString());
                    test.Add(Data2);
                }                
                ViewBag.result = Data;
                if (Data.Count < 1)
                {
                    return RedirectToAction("none", "Order_Search");
                }
                ViewBag.Book = test;
            }catch(Exception e)
            {
                return RedirectToAction("RedirectToLogin", "Login"); 
            }
            return View();
        }

        public ActionResult none()
        {
            return View();
        }


    }
}