using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class CheckController : Controller
    {
        // GET: Check
        public ActionResult Index(Model.Customer Cusdata)
        {
            Service.SQL_CustomerCheck SAC = new Service.SQL_CustomerCheck();
            List<Model.Customer> Data = new List<Model.Customer>();
            Data = SAC.Check(Cusdata);
            if (Data.Count > 0)
            {
                if (Data[0].Customer_Password == Cusdata.Customer_Password)
                {
                    HttpCookie cook = new HttpCookie("cookie");
                    cook["account"] = Cusdata.Customer_Email.ToString();
                    cook["pwd"] = Cusdata.Customer_Password.ToString();
                    Response.Cookies.Add(cook);
                    @ViewBag.show = cook["account"].ToString() + " " + cook["pwd"].ToString();
                    
                }
                else
                {
                    @ViewBag.show = "帳號或密碼錯誤";
                }
            }
            else
            {
                @ViewBag.show = "帳號或密碼錯誤";
            }
            @ViewBag.result = Data;
            return View();
        }
    }
}