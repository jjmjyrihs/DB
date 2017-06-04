using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public void  Admin_Check(string Admin_Account,string Admin_Password)
        {
            if (Admin_Account == "PleaseHackMe" && Admin_Password== "123")
            {
                Admin_Controller();
            }
            else
            {
                
            }
        }
        public ActionResult Admin_Controller()
        {
            return View();
        }
    }
}