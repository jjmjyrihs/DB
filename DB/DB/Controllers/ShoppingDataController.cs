using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class ShoppingDataController : Controller
    {
        // GET: Shopping
        public ActionResult Index(string Book_ID, string Book_Name, string Book_Price, int Book_Quantity, string Search,string Book_Img)
        {
            Service.SQL_ShippingCar SSC = new Service.SQL_ShippingCar();
            Model.ShippingCar Data = new Model.ShippingCar();
            Data.Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
            Data.Book_ID = Book_ID;
            Data.Book_Name = Book_Name;
            Data.Book_Price = Book_Price;
            Data.Book_Quantity = Book_Quantity;
            Data.Book_Img = Book_Img;
            Data.Action = "Insert";
            SSC.ShoppingCart(Data);
            return RedirectToAction("index", "Inquire", new { Book_Search = Search });
        }

        public ActionResult Delete(string Book_ID)
        {
            Service.SQL_ShippingCar SSC = new Service.SQL_ShippingCar();
            Model.ShippingCar Data = new Model.ShippingCar();
            Data.Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
            Data.Action = "Delete";
            Data.Book_ID = Book_ID;
            SSC.ShoppingCart(Data);
            return RedirectToAction("Index", "CheckOut");
        }

        public ActionResult InsertBook_Quantity(string Book_ID, string GetOrder_Quantity, string Order_ID,string Book)
        {
            Service.SQL_OrderQuantity SOQ = new Service.SQL_OrderQuantity();
            SOQ.InsertBook_Quantity(Book_ID, GetOrder_Quantity, Order_ID);
            int Order_Quantity = int.Parse(GetOrder_Quantity);
            return RedirectToAction("Index", "CheckOut", new { Order_Quantity= GetOrder_Quantity ,Book = Book});
        }
    }
}