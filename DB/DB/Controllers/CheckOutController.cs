using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        /// <summary>
        /// 確認購物車
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int Order_Quantity,string Book_ID2)
        {
            string Customer_Email = "";
            try
            {
                 Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
            }catch(Exception e)
            {
                return RedirectToAction("RedirectToLogin", "Login");
            }
            
            Service.SQL_GetShoppingCart SCO = new Service.SQL_GetShoppingCart();
            List<Model.ShippingCar> Data = new List<Model.ShippingCar>();
            Data = SCO.Find(Customer_Email);

            try
            {
                int book = int.Parse(Book_ID2);
                Data[book].Order_Quantity = Order_Quantity;
            }catch(Exception e)
            {
               
            }            
            ViewBag.result = Data;
            if (Data.Count == 0)
            {
                return RedirectToAction("NonBook", "CheckOut");
            }
            else
            {
                return View();
            }
        }

        public ActionResult NonBook()
        {
            return View();
        }
    }
}