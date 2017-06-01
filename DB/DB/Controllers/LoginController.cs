using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Check(Model.Customer Cusdata)
        {
            Service.Account_Check SAC = new Service.Account_Check();
            List<Model.Customer> Data = new List<Model.Customer>();
            Data = SAC.Check(Cusdata);
            if (Data.Count > 0)
            {
                if(Data[0].Customer_Password == Cusdata.Customer_Password)
                {
                    @ViewBag.show = "登入成功";
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