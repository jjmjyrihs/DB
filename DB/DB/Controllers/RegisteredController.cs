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

        public ActionResult Insert(string submitbutton,Model.CustomerData CusData)
        {
            switch (submitbutton)
            {
                case "已有帳號?":
                    return RedirectToAction("index", "Login");
                case "註冊":
                    Service.SQL_Registered SSR = new Service.SQL_Registered();
                    List<Model.CustomerData> Data = new List<Model.CustomerData>();
                    string get_mail  = SSR.Regis(CusData);
                    HttpCookie cook = Request.Cookies["cookie"];
                    cook["Account"] =get_mail;
                    Response.Cookies.Add(cook);
                    break;
            }
            return RedirectToAction("index", "Home");
        }
    }
}