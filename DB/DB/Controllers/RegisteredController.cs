using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class RegisteredController : Controller
    {
        // GET: Registered
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Insert(string submitbutton,Model.Customer CusData)
        {
            switch (submitbutton)
            {
                case "已有帳號?":
                    return RedirectToAction("index", "Login");
                case "註冊":
                    Service.SQL_Registered SSR = new Service.SQL_Registered();
                    List<Model.Customer> Data = new List<Model.Customer>();
                    string get_mail  = SSR.Regis(CusData);
                    HttpCookie cook = Request.Cookies["cookie"];
                    cook["account"] =get_mail;
                    Response.Cookies.Add(cook);
                    break;
            }
            return RedirectToAction("index", "Home");
        }
    }
}