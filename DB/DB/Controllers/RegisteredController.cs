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
            string Customer_Email = "";
            try
            {
                Customer_Email = Request.Cookies["cookie"]["Account"].ToString();
                if (Customer_Email != "")
                {
                    return RedirectToAction("LogOut", "Registered");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }            
        }

        public ActionResult Insert(string submitbutton,Model.CustomerData CusData)
        {
            switch (submitbutton)
            {
                case "已有帳號?":
                    return RedirectToAction("index", "Login");
                case "註冊":
                    HttpCookie cook = new HttpCookie("cookie");
                    Service.SQL_Registered SSR = new Service.SQL_Registered();
                    List<Model.CustomerData> Data = new List<Model.CustomerData>();
                    string get_mail  = SSR.Regis(CusData);
                    cook["Account"] =get_mail;
                    Response.Cookies.Add(cook);
                    break;
            }
            return RedirectToAction("index", "Home");
        }
        public ActionResult LogOut()
        {
            return View();
        }
    }
}