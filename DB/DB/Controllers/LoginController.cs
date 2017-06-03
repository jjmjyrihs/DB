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
            if (Request.Cookies["test"] == null)
            {
                return View();
            }
            else
            {
                // @ViewBag.acc = cook["account"].ToString() + "  " + cook["pwd"].ToString();
                HttpCookie cook = Request.Cookies["cookie"];
                cook.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cook);
                return RedirectToAction("index", "Home");
            }
        }
    }
}