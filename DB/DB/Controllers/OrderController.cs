using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert(Model.CustomerOrder Data,string  Order_ID)
        {
            Service.SQL_CheckOut SCO = new Service.SQL_CheckOut();
            Data.Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
            SCO.InsertCustomer_Order(Data);
            Service.SQL_GetShoppingCart SGSC = new Service.SQL_GetShoppingCart();
            List<Model.ShippingCar> Data2 = new List<Model.ShippingCar>();
            Data2  = SGSC.Find(Data.Customer_Email);
            SCO.InsertOrder_Books(Data, Data2);
            Service.SQL_DeleteShoppingCart SDSC = new Service.SQL_DeleteShoppingCart();
            SDSC.Delete_Cart();
            return RedirectToAction("Index","DataShow",new { Order_ID=Order_ID});
        }
    }
}