using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class DataShowController : Controller
    {
        // GET: DataShow
        public ActionResult Index(string Order_ID)
        {
            int Total = 0;
            Model.CustomerData Customer_Data = new Model.CustomerData();
            List<Model.CustomerOrder> Customer_Order = new List<Model.CustomerOrder>();
            List<Model.ShowBook> Book_Data = new List<Model.ShowBook>();

            Service.SQL_CustomerData SCD = new Service.SQL_CustomerData();
            Service.SQL_GetCustomerOrder SGCO = new Service.SQL_GetCustomerOrder();
            Service.SQL_GetShowBook SGSB = new Service.SQL_GetShowBook();

            Customer_Data = SCD.getData(Request.Cookies["cookie"]["Account"].ToString());
            Customer_Order = SGCO.Get_Customer_Order(Order_ID,Customer_Data.Customer_Email);
            Book_Data = SGSB.ShowBook(Customer_Order[0].Order_ID.ToString());
            ViewBag.Customer_Data = Customer_Data;
            ViewBag.Customer_Order = Customer_Order;
            for(int i = 0; i < Book_Data.Count; i++)
            {
                Total += int.Parse(Book_Data[i].Book_Price) * int.Parse(Book_Data[i].Order_Quantity);
            }
            ViewBag.Book_Data = Book_Data;
            ViewBag.Price_Total = Total;
            Service.SQL_DeleteShoppingCart SDSC = new Service.SQL_DeleteShoppingCart();
            SDSC.Delete_Cart();
            return View();
        }
        
    }
}