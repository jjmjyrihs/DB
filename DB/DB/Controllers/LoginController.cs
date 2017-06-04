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
            if (Request.Cookies["cookie"]==null)
            {
                return View();
            }
            else
            {
                // @ViewBag.acc = cook["account"].ToString() + "  " + cook["pwd"].ToString();
                HttpCookie cook =  new HttpCookie("cookie")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cook);
                return RedirectToAction("index", "Home");
            }
        }
    }
}