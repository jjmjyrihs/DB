using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        public ActionResult Index()
        {
            string Account="";
            Service.SQL_CustomerData SSC = new Service.SQL_CustomerData();
            Model.Customer Data = new Model.Customer();
            if (Request.Cookies["cookie"] == null)
            {
                @ViewBag.acc = "aa";
            }
            else
            {
                Account = Request.Cookies["cookie"]["Account"].ToString();
            }
            
            Data = SSC.getData(Account);
            ViewBag.Data = Data;
            return View();
        }

        public ActionResult Update(Model.Customer Data)
        {
            Service.SQL_CustomerUpdate SCU = new Service.SQL_CustomerUpdate();
            Boolean Check = false;
            Check = SCU.Update(Data);
            if (Check)
            {
                ViewBag.Data = "修改成功!";
            }
            else
            {
                ViewBag.Data = "修改失敗!";
            }                            
            return View();
        }
    }
}