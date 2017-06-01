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
                    SSR.Regis(CusData);
                    break;
            }
            return RedirectToAction("index", "Home");
        }
    }
}