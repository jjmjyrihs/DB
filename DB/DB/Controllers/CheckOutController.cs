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
        public ActionResult Index(int Order_Quantity=1,string Book="0")
        {
            string Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
            Service.SQL_GetShoppingCart SCO = new Service.SQL_GetShoppingCart();
            List<Model.ShippingCar> Data = new List<Model.ShippingCar>();
            Data = SCO.Find(Customer_Email);
            int book = int.Parse(Book);
            Data[book].Order_Quantity = Order_Quantity;
            ViewBag.result = Data;
            return View();
        }

        /// <summary>
        /// 結帳
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOut(Model.CustomerOrder Data)
        {
            
            Service.SQL_CheckOut SCO = new Service.SQL_CheckOut();
            
            Service.SQL_GetShoppingCart GSC = new Service.SQL_GetShoppingCart();
            List<Model.ShippingCar> GetAllBookID = new List<Model.ShippingCar>();
            GetAllBookID = GSC.Find(Request.Cookies["cookie"]["Account"].ToString());
            
            return null;
        }
    }
}